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
  final int codMunicipio;
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
      this.codMunicipio,
      this.contato,
      this.codPorte,
      this.capitalSocial,
      this.sociosEmpresa);

  Empresa.fromJson(Map<String, dynamic> json)
      : cnpj = json['cnpj'],
        isMatriz = json['isMatriz'] ?? false,
        razaoSocial = json['razaoSocial'],
        nomeFantasia = json['nomeFantasia'] ?? "",
        codSituacao = json['codSituacao'],
        codMotivo = json['codMotivo'],
        dataSituacao = json['dataSituacao'],
        codNaturezaJuridica = json['codNaturezaJuridica'],
        dataInicioAtividade = json['dataInicioAtividade'],
        codCnaeFiscal = json['codCnaeFiscal'],
        endereco = Endereco.fromJson(json['endereco']),
        codMunicipio = json['codMunicipio'],
        contato = json['contato'] == null ? null : Contato.fromJson(json['contato']),
        codPorte = json['codPorte'],
        capitalSocial = json['capitalSocial'] ?? 0,
        sociosEmpresa = json['sociosEmpresa'] ?? List.empty(growable: true);

  Map<String, dynamic> toMap() {
    var map = <String, dynamic>{
      'cnpj': cnpj,
      'isMatriz': isMatriz == true ? 1 : 0,
      'razaoSocial': razaoSocial,
      'nomeFantasia': nomeFantasia,
      'codSituacao': codSituacao,
      'codMotivo': codMotivo,
      'dataSituacao': dataSituacao,
      'codNaturezaJuridica': codNaturezaJuridica,
      'dataInicioAtividade': dataInicioAtividade,
      'codCnaeFiscal': codCnaeFiscal,
      'endereco': endereco,
      'codMunicipio': codMunicipio,
      'contato': contato,
      'codPorte': codPorte,
      'capitalSocial': capitalSocial,
      'sociosEmpresa': sociosEmpresa,
    };
    return map;
  }
}
