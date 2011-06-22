using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace Tcc_Imoveis.Models
{
	public static class Util
	{
        public static string ToPolygon(this string query)
        {
            Regex er = new Regex(@"(-[0-9\.]+).*?(-[0-9\.]+)");


            //verifica se a string do poligono está de acordo com o padrao esperado
            if (er.IsMatch(query))
            {


                MatchCollection coordinates = er.Matches(query);
                List<string> pointsPolygon = new List<string>();

                string point = string.Empty;
                for (int i = 0; i < coordinates.Count; i++)
                {
                    point = string.Format("{0} {1}", coordinates[i].Groups[1].Value, coordinates[i].Groups[2].Value);
                    pointsPolygon.Add(point);
                }


                string polygon_query = string.Empty;

                //junta os pontos separando-os por virgula
                polygon_query = string.Join(", ", pointsPolygon.ToArray());

                //fecha o poligono colocando o primeiro ponto no final do poligono.
                //Transforma em Polygon Query
                return string.Format("POLYGON(({0}, {1}))", polygon_query, pointsPolygon.ElementAt(0));



            }
            else
            {
                return null;
            }
        }
	}
}