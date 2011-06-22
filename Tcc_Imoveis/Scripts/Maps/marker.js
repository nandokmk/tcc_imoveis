
//cria um ponto no mapa e na listagem a partir de um array
function cria_imovel(imovel) {
	var latLng = new google.maps.LatLng(imovel['x'].replace(",", "."), imovel['y'].replace(",", "."));
	var marker =	cria_marker(latLng);
	google.maps.event.addListener(marker, 'click', function () {
		var url_sem_hash = location.href.split("#");
		location.href = url_sem_hash[0] + '#' + imovel['idImovel'];
	});

	adiciona_imovel_listagem(imovel);

	
}

//cria marker
var markers = Array();
function cria_marker(latLng)
{
	var marker = new google.maps.Marker({
		position: latLng,
		map: map,
		icon: "/Content/images/maps_ico_house.png"
	});

	markers[markers.length] = marker;
	return marker;
}


//adiciona um imovel na listagem html
function adiciona_imovel_listagem(info) {
	var html = '<div class="resultado1">'+
		'<a name="'+ info['idImovel'] +'">' +
		'<img src="images/fotinho.jpg" class="thumb1">' +
		'<div class="texto2">' +
		'<strong>CASA TERREA<br>' +
		info['bairro'].toUpperCase() + ' - ' + info['cidade'] + ' - ' + info['estado'] + '<br></strong>' +
		info['descricao'] +
		'</div><a href="#" class="detalhes">VEJA MAIS DETALHES</a></div>';

	$("#listagem_imoveis").append(html);
}


//limpa o mapa
function reset() {

	if(arguments.length) 
	{
		if(arguments[0] == 'polygon') 
		{
			remover_poligono();
		} 
		else if(arguments[0] == 'circ')
		{
			remover_circunferencia();
		}
	} 
	if (markers) 
	{
		for (i in markers) {
		markers[i].setMap(null);
		}
		markers.length = 0;
	}
}
