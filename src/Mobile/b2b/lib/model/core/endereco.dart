class Endereco {
  final String id;
  final String logradouro;
  final String numero;
  final String complemento;
  final String bairro;
  final String cep;

  Endereco(this.id, this.logradouro, this.numero, this.complemento, this.bairro, this.cep);

  Endereco.fromJson(this.id, Map<String, dynamic> json)
      : logradouro = json['logradouro'],
        numero = json['numero'],
        complemento = json['complemento'] ?? "",
        bairro = json['bairro'],
        cep = json['cep'];

  Map<String, dynamic> toMap() => <String, dynamic>{
        'id': id,
        'logradouro': logradouro,
        'numero': numero,
        'complemento': complemento,
        'bairro': bairro,
        'cep': cep,
      };
}
