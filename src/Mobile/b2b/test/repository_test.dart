import 'package:b2b/infra/providers.dart';
import 'package:b2b/infra/repositorio.dart';
import 'package:b2b/model/core/empresa.dart';
import 'package:b2b/model/core/municipio.dart';
import 'package:b2b/utils/services/rest_api_service.dart';
import 'package:flutter_test/flutter_test.dart';

void main() {
  group('Teste de repositorio', () {
    var repoEstado = Repository(Api('estado'));
    var reposMunicipio = Repository(Api('municipio'));
    var repoEmpresa = Repository(Api('empresa'));

    var databaseMunicipio = Repository(MunicipioProvider());
    var databaseEmpresa = Repository(EmpresaProvider());
    var databaseContato = Repository(ContatoProvider());
    var databaseEndereco = Repository(EnderecoProvider());

    List municipios = List.empty();
    List empresas = List.empty();
    test('Consulta Municipios', () async {
      municipios = await repoEstado.getList<Municipio>('SP');
      expect(municipios.isNotEmpty, isTrue);
    }, timeout: const Timeout(Duration(minutes: 5)));

    test('Consulta Empresas', () async {
      empresas = await reposMunicipio.getList<Empresa>(municipios[0].id);
      expect(empresas.isNotEmpty, isTrue);
    }, timeout: const Timeout(Duration(minutes: 5)));

    test('Salvando Municipios', () async {
      var registros = await databaseMunicipio.saveAll(municipios);
      expect(registros, isTrue);
    }, timeout: const Timeout(Duration(minutes: 5)));

    test('Salvando Empresas', () async {
      List contatos = List.empty(growable: true);
      List enderecos = List.empty(growable: true);
      for (int i = 0; i < empresas.length; i++) {
        enderecos.add(empresas[i].endereco);
        if (empresas[i].contato == null) continue;
        contatos.add(empresas[i].contato);
      }
      var registros = await databaseEmpresa.saveAll(empresas);
      var registros2 = await databaseContato.saveAll(contatos);
      var registros3 = await databaseEndereco.saveAll(enderecos);
      expect(registros, isTrue);
      expect(registros2, isTrue);
      expect(registros3, isTrue);
    }, timeout: const Timeout(Duration(minutes: 5)));
  });
}
