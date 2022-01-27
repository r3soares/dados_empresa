import 'contato.dart';
import 'endereco.dart';
import 'socio_empresa.dart';

class Empresa {
  final String cnpj;
  final bool isMatriz;
  final String razaoSocial;
  final String nomeFantasia;
  final int codSituacao;
  final int? codMotivo;
  final int dataSituacao;
  final int? codNaturezaJuridica;
  final int dataInicioAtividade;
  final int codCnaeFiscal;
  final Endereco endereco;
  final int municipio;
  final Contato? contato;
  final int codPorte;
  final double capitalSocial;
  final List<SocioEmpresa> sociosEmpresa;

  Empresa(
      this.cnpj,
      this.isMatriz,
      this.razaoSocial,
      this.nomeFantasia,
      this.codSituacao,
      this.codMotivo,
      this.dataSituacao,
      this.codNaturezaJuridica,
      this.dataInicioAtividade,
      this.codCnaeFiscal,
      this.endereco,
      this.municipio,
      this.contato,
      this.codPorte,
      this.capitalSocial,
      this.sociosEmpresa);
}
