import 'package:b2b/utils/services/local_storage_service.dart';

class EstadoProvider extends Provider {
  EstadoProvider()
      : super(
          'estado',
          'uf',
        );
}

class MunicipioProvider extends Provider {
  MunicipioProvider()
      : super(
          'municipio',
          'id',
        );
}

class EmpresaProvider extends Provider {
  EmpresaProvider()
      : super(
          'empresa',
          'cnpj',
        );
}

class EnderecoProvider extends Provider {
  EnderecoProvider()
      : super(
          'endereco',
          'id',
        );
}

class ContatoProvider extends Provider {
  ContatoProvider()
      : super(
          'contato',
          'id',
        );
}
