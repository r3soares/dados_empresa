import 'dart:io';
import 'dart:math';

import 'package:b2b/infra/repositorio.dart';
import 'package:b2b/utils/services/rest_api_service.dart';
import 'package:flutter_test/flutter_test.dart';

void main() {
  group('Teste de repositorio', () {
    var repoEstado = Repository(Api('v1/estado'));
    var reposMunicipio = Repository(Api('v1/municipio'));
    test('Consulta Estados', () async {
      var municipios = await repoEstado.get('SP');
    }, timeout: const Timeout(Duration(minutes: 5)));
  });
}
