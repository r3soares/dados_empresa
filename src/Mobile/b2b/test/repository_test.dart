import 'package:b2b/infra/providers.dart';
import 'package:b2b/infra/repositorio.dart';
import 'package:b2b/model/core/empresa.dart';
import 'package:b2b/model/core/municipio.dart';
import 'package:b2b/utils/services/local_storage_service.dart';
import 'package:b2b/utils/services/rest_api_service.dart';
import 'package:flutter_test/flutter_test.dart';

void main() {
  group('Teste de repositorio', () {
    var repoEstado = Repository(Api('estado'));
    var reposMunicipio = Repository(Api('municipio'));
    var repoEmpresa = Repository(Api('empresa'));

    var databaseMunicipio = Repository(MunicipioProvider());
    var databaseEmpresa = Repository(EmpresaProvider());

    List municipios = List.empty();
    List empresas = List.empty();
    test('Consulta Municipios', () async {
      municipios = await repoEstado.getList<Municipio>('SP') as List;
      expect(municipios.isNotEmpty, isTrue);
    }, timeout: const Timeout(Duration(minutes: 5)));

    test('Consulta Empresas', () async {
      empresas = await reposMunicipio.getList<Empresa>(municipios[0].id);
      expect(empresas.isNotEmpty, isTrue);
    }, timeout: const Timeout(Duration(minutes: 5)));

    test('Salvando Municipios', () async {
      var registros = await databaseMunicipio.saveAll(municipios);
      expect(empresas.isNotEmpty, isTrue);
    }, timeout: const Timeout(Duration(minutes: 5)));

    test('Salvando Empresas', () async {
      var registros = await databaseMunicipio.saveAll(municipios);
      expect(empresas.isNotEmpty, isTrue);
    }, timeout: const Timeout(Duration(minutes: 5)));
  });
}
