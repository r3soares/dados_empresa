import 'telefone.dart';

class Contato {
  final String id;
  final List<Telefone> telefones;
  final String? email;

  Contato(this.id, this.telefones, this.email);

  Contato.fromJson(this.id, Map<String, dynamic> json)
      : telefones = (json['telefones'] as List).map((n) => Telefone.fromJson(n)).toList(),
        email = json['email'];
}
