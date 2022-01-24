//We do call the rest API to get, store data on a remote database for that we need to write the rest API call
//at a single place and need to return the data if the rest call is a success or need to return custom error 
//exception on the basis of 4xx, 5xx status code. 
//We can make use of http package to make the rest API call in the flutter

import 'dart:convert';
import 'dart:io';
import 'package:b2b/domain/erros.dart';
import 'package:b2b/domain/log.dart';
import 'package:b2b/utils/services/iDatabase.dart';
import 'package:http/http.dart' as http;

import 'package:http/http.dart';


class Api implements IDatabase {
  final String endereco = 'https://10.50.46.41:45455/api/';
  final String controller;
  final headers = {
    'Content-Type': 'application/json',
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Methods': 'GET, POST, DELETE, PUT, OPTIONS',
    'Access-Control-Allow-Headers': 'X-Requested-With',
    'Accept-Encoding': 'gzip',
  };

  Api(this.controller);
  @override
  delete(id) async {
    try {
      final uri = Uri.parse('$endereco$controller/$id');
      final req = await _request(tipo: TipoRequest.Delete, uri: uri);
      return _resposta(req);
    } catch (e) {
      Log.message(this, e.toString());
      rethrow;
    }
  }

  @override
  getAll() async {
    try {
      final uri = Uri.parse(endereco + controller);
      final req = await _request(tipo: TipoRequest.Get, uri: uri);
      return _resposta(req);
    } catch (e) {
      Log.message(this, e.toString());
      rethrow;
    }
  }

  @override
  getById(id) async {
    try {
      final uri = Uri.parse('$endereco$controller/$id');
      //print('Api $uri');
      final req = await _request(tipo: TipoRequest.Get, uri: uri);
      return _resposta(req);
    } catch (e) {
      Log.message(this, e.toString());
      rethrow;
    }
  }

  @override
  find(instrucao, termo) async {
    try {
      final uri = Uri.parse('$endereco$controller/$instrucao/$termo');
      //print(uri);
      final req = await _request(tipo: TipoRequest.Get, uri: uri);
      return _resposta(req);
    } catch (e) {
      Log.message(this, e.toString());
      rethrow;
    }
  }

  @override
  find2(instrucao, data) async {
    try {
      final uri = Uri.parse('$endereco$controller/$instrucao');
      //print(uri);
      final req = await _request(tipo: TipoRequest.Post, uri: uri, data: data);
      return _resposta(req);
    } catch (e) {
      Log.message(this, e.toString());
      rethrow;
    }
  }

  @override
  save(data) async {
    try {
      final uri = Uri.parse(endereco + controller);
      final req = await _request(tipo: TipoRequest.Post, uri: uri, data: data);
      return _resposta(req);
    } catch (e) {
      Log.message(this, e.toString());
      rethrow;
    }
  }

  @override
  update(data) async {
    try {
      final uri = Uri.parse(endereco + controller);
      final req = await _request(tipo: TipoRequest.Put, uri: uri, data: data);
      return _resposta(req);
    } catch (e) {
      Log.message(this, e.toString());
      rethrow;
    }
  }

  _request({required tipo, required Uri uri, String data = ''}) async {
    try {
      switch (tipo) {
        case TipoRequest.Get:
          {
            return await http.get(uri, headers: headers);
          }
        case TipoRequest.Post:
          {
            return await http.post(uri, body: data, headers: headers);
          }
        case TipoRequest.Put:
          {
            return await http.post(uri, body: data, headers: headers);
          }
        case TipoRequest.Delete:
          {
            return await http.delete(uri, headers: headers);
          }
      }
    } on SocketException catch (e) {
      throw ErroConexao(e.message);
    } on Exception catch (e) {
      throw Falha(e);
    }
  }

  _resposta(Response req) {
    switch (req.statusCode) {
      case 200: //Ok
        {
          return jsonDecode(req.body, reviver: (key, value) {
            // switch (key) {
            //   case "compartimentos":
            //     {
            //       return (value as List).map((e) => Compartimento.fromJson(e)).toList();
            //     }
            //   case "tanquesAgendados":
            //     {
            //       return (value as List).map((e) => TanqueAgendado.fromJson(e)).toList();
            //     }
            // }
            return value;
          });
        }
      case 202: //Accepted
        {
          return true;
        }
      case 204: //No Content
        {
          return false;
        }
      case 400: //Bad Request
        {
          throw ErroRequisicao('Bad Request (400)');
        }
      case 404: //Not Found
        {
          throw NaoEncontrado('Not Found (404)');
        }
      case 500: //Internal Server Error
        {
          throw ErroServidor('Internal Server Error (500)'); //Exception('Erro interno do servidor');
        }
    }
    throw Falha('Código de retorno não esperados: ${req.statusCode}');
  }
}

enum TipoRequest { Get, Post, Put, Delete }
