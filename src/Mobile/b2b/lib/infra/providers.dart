import 'package:b2b/utils/services/local_storage_service.dart';

class EstadoProvider extends Provider {
  EstadoProvider() : super('''create table estado (uf text primary key)''', 'estado', 'uf');
}
