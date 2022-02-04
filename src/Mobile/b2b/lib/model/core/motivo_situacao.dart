class MotivoSituacao {
  final int cod;
  final String descricao;

  MotivoSituacao(this.cod, this.descricao);

  MotivoSituacao.fromJson(Map<String, dynamic> json)
      : cod = json['cod'],
        descricao = json['descricao'];

  Map<String, dynamic> toMap() => <String, dynamic>{'cod': cod, 'descricao': descricao};
}
