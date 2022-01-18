import 'package:b2b/domain/erros.dart';
import 'package:b2b/domain/log.dart';
import 'package:b2b/infra/IDatabase.dart';
import 'package:b2b/model/core/estado.dart';

class RepositoryTanque {
  final IDatabase db;

  RepositoryTanque(this.db);

  Future<Estado> findEmpresasByUF(String uf) async {
    try {
      var result = await db.find('uf', uf);
      var tanque = result == false ? throw NaoEncontrado(uf) : Estado.fromJson(result);
      return tanque;
    } on Falha catch (e) {
      Log.message(this, 'Erro ao procurar empresa pelo uf $uf: ${e.msg}');
      rethrow;
    }
  }

  Future<List<Estado>> findTanquesByPlacaParcial(String placa) async {
    try {
      var result = await db.find('placaParcial', placa);
      return result == false ? List.empty(growable: true) : (result as List).map((n) => Estado.fromJson(n)).toList();
    } on Falha catch (e) {
      Log.message(this, 'Erro ao procurar tanques pela placa parcial $placa: ${e.msg}');
      rethrow;
    }
  }

  Future<List<Estado>> findTanquesByInmetroParcial(String inmetro) async {
    try {
      var result = await db.find('inmetroParcial', inmetro);
      return result == false ? List.empty(growable: true) : (result as List).map((n) => Estado.fromJson(n)).toList();
    } on Falha catch (e) {
      Log.message(this, 'Erro ao procurar tanques pelo cod inmetro parcial $inmetro: ${e.msg}');
      rethrow;
    }
  }

  Future<List<Estado>> findTanquesByProprietario(String proprietario) async {
    try {
      var result = await db.find('proprietario', proprietario);
      return result == false ? List.empty(growable: true) : (result as List).map((n) => Estado.fromJson(n)).toList();
    } on Falha catch (e) {
      Log.message(this, 'Erro ao procurar tanques pelo proprietário $proprietario: ${e.msg}');
      rethrow;
    }
  }

  Future<Estado> getTanque(String inmetro) async {
    try {
      var result = await db.getById(inmetro);
      var tanque = result == false ? throw NaoEncontrado(inmetro) : Estado.fromJson(result);
      return tanque;
    } on Falha catch (e) {
      Log.message(this, 'Erro ao procurar tanque $inmetro: ${e.msg}');
      rethrow;
    }
  }
}
