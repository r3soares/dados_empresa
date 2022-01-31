class Endereco {
  final String logradouro;
  final String numero;
  final String complemento;
  final String bairro;
  final String cep;

  Endereco(this.logradouro, this.numero, this.complemento, this.bairro, this.cep);

  Endereco.fromJson(Map<String, dynamic> json)
      : logradouro = json['logradouro'],
        numero = json['numero'],
        complemento = json['complemento'] ?? "",
        bairro = json['bairro'],
        cep = json['cep'];
}
