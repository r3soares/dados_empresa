import 'dart:io';
import 'dart:math';

import 'package:b2b/infra/repositorio.dart';
import 'package:b2b/model/core/municipio.dart';
import 'package:b2b/utils/services/rest_api_service.dart';
import 'package:flutter_test/flutter_test.dart';

void main() {
  group('Teste de repositorio', () {
    var repoEstado = Repository(Api('estado'));
    var reposMunicipio = Repository(Api('municipio'));
    test('Consulta Estados', () async {
      var municipios = await repoEstado.getList<Municipio>('SP');
      expect(municipios.isNotEmpty, isTrue);
    }, timeout: const Timeout(Duration(minutes: 5)));
  });
}
