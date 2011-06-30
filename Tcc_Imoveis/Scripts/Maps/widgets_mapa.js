
/*************************************
 * TRABALHA COM POLIGONO
**************************************/

//habilita o widget para desenhar poligono no mapa.
function adicionar_busca_poligono()
{
	remover_circunferencia();
	if(!creator)
	{
		creator = new PolygonCreator(map);
	}
}


//remove o poligono do mapa caso ele esteja criado
function remover_poligono()
{
	if(creator){
		creator.destroy();
		creator=null;
	}
}

//essa funcao é usada para desenhar um poligono com pontos já existentes
//por exemplo quando usuario salva um poligono na sua busca
function desenha_poligono_pronto(id) {
        $.ajax({ type: "POST", url: "/map/ListaResult/"+id, success: function (json) {
            var polygonLatLng = Array();


            var aLat = Array();
            var aLng = Array();
            var polygon_query = "";
            $.each(json, function (i, val) {
                polygonLatLng[i] = new google.maps.LatLng(val[0], val[1]);
                aLat[i] = val[0];
                aLng[i] = val[1];

                polygon_query += val[0] + " " + val[1] + ", "
            });

            var userPolygon = new google.maps.Polygon({
                paths: polygonLatLng,
                strokeColor: "#FF0000",
                strokeOpacity: 0.8,
                strokeWeight: 2,
                fillColor: "#FF0000",
                fillOpacity: 0.35
            });

            userPolygon.setMap(map);


            //define um retangulo máximo que caiba o poligono no mapa
            var latlngBounds = new google.maps.LatLngBounds(
                new google.maps.LatLng(Array.min(aLat), Array.min(aLng)),
                new google.maps.LatLng(Array.max(aLat), Array.max(aLng))
           );
            //reconfigura o mapa para exibir o poligono inteiro na tela. 
            map.fitBounds(latlngBounds);

            //exibe(polygon_query);

        }
        });
    }





/*************************************
 * TRABALHA COM CIRFUNFERENCIA
**************************************/

//retira o circunferencia do mapa caso ela esteja criada
function remover_circunferencia()
{
	if(distanceWidget)
	{
		distanceWidget.marker.setMap(null);
		distanceWidget = null;        
	}
}

//habilita o widget de circunferencia no mapa.
function adicionar_busca_raio()
{
	remover_poligono();
	distanceWidget = new DistanceWidget({
    map: map,
    distance: $("#default_distance").val(), //define o raio em km da circunferencia
    maxDistance: 10,
    color: '#000000',
    activeColor: '#5599bb',
    icon: '/Content/images/maps_center_icon.png',
    sizerIcon: new google.maps.MarkerImage('http://code.google.com/intl/pt-BR/apis/maps/articles/mvcfun/resize-off.png'),
    activeSizerIcon: new google.maps.MarkerImage('http://code.google.com/intl/pt-BR/apis/maps/articles/mvcfun/resize.png')
  });


  map.fitBounds(distanceWidget.get('bounds'));
  //google.maps.event.addListener(distanceWidget, 'position_changed', updatePosition);
}

