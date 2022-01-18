import 'socio_empresa.dart';
import 'tipo_socio.dart';

class Socio {
  final List<SocioEmpresa> socioEmpresas;
  final String nome;
  final String cnpjCpf;
  final TipoSocio tipo;

  Socio(this.socioEmpresas, this.nome, this.cnpjCpf, this.tipo);
}
