class TipoSocio {
  /// <summary>
  /// 1 – PESSOA JURÍDICA
  //  2 – PESSOA FISICA
  //  3 – ESTRANGEIRO
  /// </summary>
  final int cod;
  final String descricao;

  TipoSocio(this.cod, this.descricao);

  TipoSocio.fromJson(Map<String, dynamic> json)
      : cod = json['cod'],
        descricao = json['descricao'];

  Map<String, dynamic> toMap() => <String, dynamic>{
        'cod': cod,
        'descricao': descricao,
      };
}
