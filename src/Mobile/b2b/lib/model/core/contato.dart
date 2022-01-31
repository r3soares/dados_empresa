import 'telefone.dart';

class Contato {
  final List<Telefone> telefones;
  final String? email;

  Contato(this.telefones, this.email);

  Contato.fromJson(Map<String, dynamic> json)
      : telefones = (json['telefones'] as List).map((n) => Telefone.fromJson(n)).toList(),
        email = json['email'];
}
