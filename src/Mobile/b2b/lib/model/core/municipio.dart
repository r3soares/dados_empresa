import 'empresa.dart';
import 'estado.dart';

class Municipio {
  final int id;
  final String nome;
  final Estado uf;
  final List<Empresa> empresas;

  Municipio(this.id, this.nome, this.uf, this.empresas);
}
