import 'package:b2b/domain/erros.dart';
import 'package:b2b/domain/log.dart';
import 'package:b2b/model/core/empresa.dart';
import 'package:b2b/model/core/estado.dart';
import 'package:b2b/model/core/municipio.dart';
import 'package:b2b/model/core/socio_empresa.dart';
import 'package:b2b/utils/services/iDatabase.dart';

class Repository {
  final IDatabase db;

  Repository(this.db);

  Future getAll<T>() async {
    try {
      var result = await db.getAll();
      return result == false ? List.empty(growable: true) : (result as List).map((n) => _fromJson<T>(n)).toList();
    } on Falha catch (e) {
      Log.message(this, 'Erro ao buscar tudo em ${T.runtimeType.toString()}: ${e.msg}');
      rethrow;
    }
  }

  Future get<T>(id) async {
    try {
      var result = await db.getById(id);
      var dado = result == false ? throw NaoEncontrado(id) : _fromJson<T>(result);
      return dado;
    } on Falha catch (e) {
      Log.message(this, 'Erro ao procurar pelo id $id: ${e.msg}');
      rethrow;
    }
  }

  Future<List> getList<T>(id) async {
    try {
      var result = await db.getById(id);
      return result == false ? List.empty(growable: true) : (result as List).map((n) => _fromJson<T>(n)).toList();
    } on Falha catch (e) {
      Log.message(this, 'Erro ao procurar pelo $id: ${e.msg}');
      rethrow;
    }
  }

  Future<bool> save(value) async {
    try {
      bool salvou = (await db.save(value.toMap())) > 0;
      if (!salvou) Log.message(this, 'Dado não foi salvo: $value');
      return salvou;
    } on Falha catch (e) {
      Log.message(this, 'Erro ao salvar dado $value: ${e.msg}');
      rethrow;
    }
  }

  saveAll(List value) async {
    try {
      var lista = List.generate(value.length, (index) => value[index].toMap());
      bool salvou = await db.saveAll(lista);
      if (!salvou) Log.message(this, 'Dado não foi salvo: $value');
      return salvou;
    } on Falha catch (e) {
      Log.message(this, 'Erro ao salvar dado $value: ${e.msg}');
      rethrow;
    }
  }

  dynamic _fromJson<T>(dynamic result) {
    switch (T) {
      case Estado:
        {
          return Estado.fromJson(result);
        }
      case Municipio:
        {
          return Municipio.fromJson(result);
        }
      case Empresa:
        {
          return Empresa.fromJson(result);
        }
      case SocioEmpresa:
        {
          return SocioEmpresa.fromJson(result);
        }
      // case Contato:
      //   {
      //     return Contato.fromJson(result);
      //   }
      // case Endereco:
      //   {
      //     return Endereco.fromJson(result);
      //   }
    }
  }
}
