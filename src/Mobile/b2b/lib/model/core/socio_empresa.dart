import 'empresa.dart';
import 'qualificacao_responsavel.dart';
import 'socio.dart';

class SocioEmpresa {
  final Socio socio;
  final Empresa empresa;
  final QualificacaoResponsavel qualificacao;
  final int dataEntrada;
  final double capital;

  SocioEmpresa(this.socio, this.empresa, this.qualificacao, this.dataEntrada, this.capital);
}
