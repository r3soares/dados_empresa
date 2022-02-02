import 'package:b2b/utils/services/local_storage_service.dart';

class EstadoProvider extends Provider {
  EstadoProvider()
      : super(
          '''CREATE TABLE estado (uf TEXT PRIMARY KEY)''',
          'estado',
          'uf',
        );
}

class MunicipioProvider extends Provider {
  MunicipioProvider()
      : super(
          '''CREATE TABLE municipio (id INTEGER PRIMARY KEY, nome TEXT, uf TEXT)''',
          'municipio',
          'id',
        );
}
