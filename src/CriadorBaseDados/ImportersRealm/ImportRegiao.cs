using CriadorBaseDados.Model.DB;
using CriadorBaseDados.Model.DB.CNAE;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CriadorBaseDados.ImportersRealm
{
    public class ImportRegiao
    {
        public Estado ImportEstado(string uf, Realm baseCompleta)
        {
            return baseCompleta.Find<Estado>(uf);
        }
        public List<int> ImportCNAEs(string[] secoes, int[] divisoes, int[] grupos, int[] classes, Realm baseCompleta)
        {
            List<int> subclasses = new List<int>();
            foreach (var s in secoes)
            {
                var temp = baseCompleta.Find<CnaeSecao>(s);
                if (temp != null)
                { 
                    foreach (var d in temp.Divisoes)
                    {
                        foreach(var g in d.Grupos)
                        {
                            foreach(var c in g.Classes)
                            {
                                foreach(var sc in c.Subclasses)
                                {
                                    subclasses.Add(sc.ID);
                                }
                            }
                        }
                    }
                }
            }
            foreach (var d in divisoes)
            {
                var temp = baseCompleta.Find<CnaeDivisao>(d);
                if (temp != null)
                    foreach (var g in temp.Grupos)
                    {
                        foreach (var c in g.Classes)
                        {
                            foreach (var sc in c.Subclasses)
                            {
                                subclasses.Add(sc.ID);
                            }
                        }
                    }
            }
            foreach (var g in grupos)
            {
                var temp = baseCompleta.Find<CnaeGrupo>(g);
                if (temp != null)
                    foreach (var c in temp.Classes)
                    {
                        foreach (var sc in c.Subclasses)
                        {
                            subclasses.Add(sc.ID);
                        }
                    }
            }
            foreach (var c in classes)
            {
                var temp = baseCompleta.Find<CnaeClasse>(c);
                if (temp != null)
                    foreach (var sc in temp.Subclasses)
                    {
                        subclasses.Add(sc.ID);
                    }
            }

            return subclasses;
        }

        public void GetEmpresasNoCnae(IQueryable<Empresa> empresas, IDictionary<int,CnaeSubclasse> cnae)
        {

        }
    }
}
