import 'cnae/cnae_subclasse.dart';
import 'contato.dart';
import 'endereco.dart';
import 'motivo_situacao.dart';
import 'natureza_juridica.dart';
import 'porte_empresa.dart';
import 'situacao_cadastral.dart';
import 'socio_empresa.dart';

class Empresa {
  final String cnpj;
  final bool isMatriz;
  final String razaoSocial;
  final String nomeFantasia;
  final SituacaoCadastral situacao;
  final MotivoSituacao motivo;
  final int dataSituacao;
  final NaturezaJuridica naturezaJuridica;
  final int dataInicioAtividade;
  final CnaeSubclasse cnaeFiscal;
  final Endereco endereco;
  final int municipio;
  final Contato contato;
  final PorteEmpresa porte;
  final double capitalSocial;
  final List<SocioEmpresa> sociosEmpresa;

  Empresa(
      this.cnpj,
      this.isMatriz,
      this.razaoSocial,
      this.nomeFantasia,
      this.situacao,
      this.motivo,
      this.dataSituacao,
      this.naturezaJuridica,
      this.dataInicioAtividade,
      this.cnaeFiscal,
      this.endereco,
      this.municipio,
      this.contato,
      this.porte,
      this.capitalSocial,
      this.sociosEmpresa);
}
