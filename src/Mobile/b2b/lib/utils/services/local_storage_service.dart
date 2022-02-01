//In this file, we write all the code needed to store and get data from the local storage using the plugin shared_preferences.
//In this file, there will be getters and setters for each and every data to be stored in the local storage.
import 'package:b2b/model/core/estado.dart';
import 'package:b2b/utils/services/iDatabase.dart';
import 'package:sqflite/sqflite.dart';
import 'package:sqflite/sqlite_api.dart';

class Provider extends IDatabase {
  Database? db;
  final String createTable;
  final tableName;
  final columnId;

  Provider(this.createTable, this.tableName, this.columnId);

  Future open(String path) async {
    db = await openDatabase(path, version: 1, onCreate: (Database db, int version) async {
      await db.execute(createTable);
    });
  }

  @override
  delete(id) async {
    return await db!.delete(tableName, where: '$columnId = ?', whereArgs: [id]);
  }

  @override
  find(instrucao, termo) async {
    List<Map> maps = await db!.query(tableName, where: '$instrucao = ?', whereArgs: [termo]);
    if (maps.isNotEmpty) {
      return List.generate(maps.length, (index) => _fromMap(maps[index]));
    }
    return null;
  }

  @override
  find2(instrucao, termo) async {
    List<Map> maps = await db!.query(tableName, where: '$instrucao LIKE ?', whereArgs: [termo]);
    if (maps.isNotEmpty) {
      return List.generate(maps.length, (index) => _fromMap(maps[index]));
    }
    return null;
  }

  @override
  getAll() async {
    List<Map> maps = await db!.query(tableName);
    if (maps.isNotEmpty) {
      return List.generate(maps.length, (index) => _fromMap(maps[index]));
    }
    return null;
  }

  @override
  getById(id) async {
    List<Map> maps = await db!.query(tableName, where: '$columnId = ?', whereArgs: [id]);
    if (maps.isNotEmpty) {
      return _fromMap(maps.first);
    }
    return null;
  }

  @override
  save(data) async {
    return await db!.insert(tableName, data.toMap());
  }

  @override
  update(data) {
    // TODO: implement update
    throw UnimplementedError();
  }

  _fromMap(Map<dynamic, dynamic> map) {
    switch (tableName) {
      case 'estado':
        return Estado.fromMap(map);
    }
  }
}
