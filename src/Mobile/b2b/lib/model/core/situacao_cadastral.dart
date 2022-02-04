class SituacaoCadastral {
  /// <summary>
  /// 2 DIGITOS
  //CÓDIGO DA SITUAÇÃO CADASTRAL
  //01 - NULA
  //02 - ATIVA
  //03 - SUSPENSA
  //04 - INAPTA
  //08 - BAIXADA
  /// </summary>
  final int cod;
  final String descricao;

  SituacaoCadastral(this.cod, this.descricao);

  SituacaoCadastral.fromJson(Map<String, dynamic> json)
      : cod = json['cod'],
        descricao = json['descricao'];

  Map<String, dynamic> toMap() => <String, dynamic>{
        'cod': cod,
        'descricao': descricao,
      };
}
