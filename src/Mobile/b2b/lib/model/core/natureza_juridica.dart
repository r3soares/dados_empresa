class NaturezaJuridica {
  final int cod;
  final int codGrupo;
  final String descricao;

  NaturezaJuridica(this.cod, this.codGrupo, this.descricao);

  NaturezaJuridica.fromJson(Map<String, dynamic> json)
      : cod = json['cod'],
        codGrupo = json['codGrupo'],
        descricao = json['descricao'];

  Map<String, dynamic> toMap() => <String, dynamic>{
        'cod': cod,
        'codGrupo': codGrupo,
        'descricao': descricao,
      };
}
