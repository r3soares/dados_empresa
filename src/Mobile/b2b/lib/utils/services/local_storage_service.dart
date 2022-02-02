//In this file, we write all the code needed to store and get data from the local storage using the plugin shared_preferences.
//In this file, there will be getters and setters for each and every data to be stored in the local storage.
import 'dart:io';

import 'package:b2b/constants/app_constants.dart';
import 'package:b2b/domain/erros.dart';
import 'package:b2b/utils/services/iDatabase.dart';
import 'package:sqflite/sqflite.dart';
import 'package:sqflite_common_ffi/sqflite_ffi.dart';

class Provider extends IDatabase {
  Database? _db;
  final String createTable;
  final String tableName;
  final String columnId;
  static bool factoryIniciou = false;

  Provider(this.createTable, this.tableName, this.columnId) {
    if (!factoryIniciou && Platform.isWindows || Platform.isLinux) {
      // Initialize FFI
      sqfliteFfiInit();
      // Change the default factory
      databaseFactory = databaseFactoryFfi;
      factoryIniciou = true;
    }
  }

  Future _open() async {
    if (_db != null && _db!.isOpen) return;
    var path = await getDatabasesPath() + Constants.databaseName;
    try {
      _db = await openDatabase(path, version: 1, onCreate: (Database db, int version) async {
        await db.execute(createTable);
      });
    } on Exception catch (e) {
      throw Falha('Erro ao abrir database: $e');
    }
  }

  Future _close() async => _db!.close();

  @override
  delete(id) async {
    try {
      await _open();
      return await _db!.delete(tableName, where: '$columnId = ?', whereArgs: [id]);
    } on Exception catch (e) {
      throw Falha('Erro ao deletar tabela $tableName id $id: $e');
    } finally {
      _close();
    }
  }

  @override
  find(instrucao, termo) async {
    try {
      await _open();
      return await _db!.query(tableName, where: '$instrucao = ?', whereArgs: [termo]);
    } on Exception catch (e) {
      throw Falha('Erro ao buscar $instrucao na tabela $tableName com o termo $termo: $e');
    } finally {
      _close();
    }
  }

  @override
  find2(instrucao, termo) async {
    try {
      await _open();
      return await _db!.query(tableName, where: '$instrucao LIKE ?', whereArgs: [termo]);
    } on Exception catch (e) {
      throw Falha('Erro ao buscar (LIKE) $instrucao na tabela $tableName com o termo $termo: $e');
    } finally {
      _close();
    }
  }

  @override
  getAll() async {
    try {
      await _open();
      return await _db!.query(tableName);
    } on Exception catch (e) {
      throw Falha('Erro ao buscar tabela $tableName: $e');
    } finally {
      _close();
    }
  }

  @override
  getById(id) async {
    try {
      await _open();
      return await _db!.query(tableName, where: '$columnId = ?', whereArgs: [id]);
    } on Exception catch (e) {
      throw Falha('Erro ao buscar id $id da tabela $tableName: $e');
    } finally {
      _close();
    }
  }

  @override
  save(data) async {
    try {
      await _open();
      return await _db!.insert(tableName, data, conflictAlgorithm: ConflictAlgorithm.replace);
    } on Exception catch (e) {
      throw Falha('Erro ao salvar dado da tabela $tableName. Dado:\n$data\nErro:\n: $e');
    } finally {
      _close();
    }
  }

  @override
  saveAll(data) async {
    try {
      await _open();
      var batch = _db!.batch();
      for (var element in data) {
        batch.insert(tableName, element, conflictAlgorithm: ConflictAlgorithm.replace);
      }

      await batch.commit(noResult: true);
      return true;
    } on Exception catch (e) {
      throw Falha('Erro ao salvar dados da tabela $tableName: $e');
    } finally {
      _close();
    }
  }

  @override
  update(data) {
    // TODO: implement update
    throw UnimplementedError();
  }

  // _fromMap(Map<dynamic, dynamic> map) {
  //   switch (tableName) {
  //     case 'estado':
  //       return Estado.fromMap(map);
  //     case 'municipio':
  //       return Municipio.fromMap(map);
  //   }
  // }
}
