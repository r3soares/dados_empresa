class QualificacaoResponsavel {
  final int cod;
  final String descricao;

  QualificacaoResponsavel(this.cod, this.descricao);

  QualificacaoResponsavel.fromJson(Map<String, dynamic> json)
      : cod = json['cod'],
        descricao = json['descricao'];

  Map<String, dynamic> toMap() => <String, dynamic>{
        'cod': cod,
        'descricao': descricao,
      };
}
