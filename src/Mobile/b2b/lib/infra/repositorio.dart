import 'package:b2b/domain/erros.dart';
import 'package:b2b/domain/log.dart';
import 'package:b2b/model/core/json_serializable.dart';
import 'package:b2b/utils/services/iDatabase.dart';

class Repository<T> {
  final IDatabase db;

  Repository(this.db);

  Future getAll() async {
    try {
      var result = await db.getAll();
      return result == false
          ? List.empty(growable: true)
          : (result as List).map((n) => (T as JsonSerializable).fromJson(n)).toList();
    } on Falha catch (e) {
      Log.message(this, 'Erro ao buscar tudo em ${T.runtimeType.toString()}: ${e.msg}');
      rethrow;
    }
  }

  Future get(dynamic id) async {
    try {
      var result = await db.getById(id);
      var dado = result == false ? throw NaoEncontrado(id) : (T as JsonSerializable).fromJson(result);
      return dado;
    } on Falha catch (e) {
      Log.message(this, 'Erro ao procurar pelo id $id: ${e.msg}');
      rethrow;
    }
  }

  Future<List> getList(dynamic id) async {
    try {
      var result = await db.getById(id);
      return result == false
          ? List.empty(growable: true)
          : (result as List).map((n) => (T as JsonSerializable).fromJson(n)).toList();
    } on Falha catch (e) {
      Log.message(this, 'Erro ao procurar pelo $id: ${e.msg}');
      rethrow;
    }
  }
}
