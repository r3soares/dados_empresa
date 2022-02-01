class SocioEmpresa {
  final String cnpjSocio;
  final String cnpjEmpresa;
  final int codQualificacao;
  final int dataEntrada;
  final double capital;

  SocioEmpresa(this.cnpjSocio, this.cnpjEmpresa, this.codQualificacao, this.dataEntrada, this.capital);

  SocioEmpresa.fromJson(Map<String, dynamic> json)
      : cnpjSocio = json['cnpjSocio'],
        cnpjEmpresa = json['cnpjEmpresa'],
        codQualificacao = json['codQualificacao'],
        dataEntrada = json['dataEntrada'],
        capital = json['capital'];
}
