import 'socio_empresa.dart';

class Socio {
  final List<SocioEmpresa> socioEmpresas;
  final String nome;
  final String cnpjCpf;
  final int codTipo;

  Socio(this.socioEmpresas, this.nome, this.cnpjCpf, this.codTipo);
}
