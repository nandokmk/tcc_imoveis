
//inicializa e cria o mapa
function init()
{
	//define o ponto central
	var myLatlng = new google.maps.LatLng(-23.5556631, -46.6903586);
  var myOptions = {
      zoom: 10,
      center: myLatlng,
      mapTypeId: google.maps.MapTypeId.ROADMAP
  }
	//cria o mapa
  map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
}

//procura um ponto a partir de um endereço digitado
//por exemplo Av Paulista, 3000 - sao Paulo, SP
//vai procurar o endereço, retornar uma latitude e longitude e centralizar o mapa neste ponto.
function centralizar_mapa(endereco)
{

 geocoder = new google.maps.Geocoder();
 geocoder.geocode({ address: endereco }, function (results, status) {
		//verifica se encontrou o endereco
    if (status == google.maps.GeocoderStatus.OK) {
				
				//centraliza o mapa no ponto buscado        
				map.fitBounds(results[0].geometry.viewport);
				
				//cria uma marcação no mapa
				cria_marker(results[0].geometry.location)

				//exibe imoveis nas proximidades
        exibir_pontos();
        
    } 
		else
	 {
        alert("Endereço não encontrado");
    }
 });
 
 
 
}

function exibir_pontos() 
{       
           
	//verifica se existe um poligono criado.
	//se existir configura a url e querystring para fazer uma consulta a busca poligonal.
	if(creator)
	{
		executar_busca("/map/PolygonResult", "polygon="+creator.showData());
	}
	//verifica se existe um widget de distancia (raio) criado
	else if(distanceWidget) 
	{
		//pega o raio da circunferencia
		var d = distanceWidget.get('distance');
		//pega o ponto central da circunferencia
		var p = distanceWidget.get('position');

		executar_busca("/map/DistanceResult", "distance="+d+"&p="+p);
	 
	}
	//caso contrario exibe imoveis num raio de 800metros a partir do centro do mapa.
	else 
	{
		if($("#select_distancia").val() != 0) {
			alert("distancia");
			var distancia = $("#select_distancia").val()
		} else {
			var distancia = 0.5;
		}
		executar_busca("/map/DistanceResult", "distance="+  distancia +"&p="+map.getCenter())
	} 
            
}


function executar_busca(url, q)
{
	$.ajax({type: "POST", url: url, data: q,
		success: function(json){
				//limpa o mapa		    
				reset();
		    $.each(json, function(i, imovel)
				{
						//verifica se o ponto não está vazio
		        if(imovel['x'] != undefined) 
						{
							cria_imovel(imovel);
		        }
		        
		    });
		}
	});
}

function excluir_imovel_pesquisa(idImovel)
{
	$.post("/Map/ExcluirImovelPesquisa", { 'idImovel': idImovel}, 
	function(json){		
		//limpa todos os mapas
		reset();
		$.each(json, function(i, imovel){
			//verifica se o ponto não está vazio
			if(imovel['x'] != undefined) 
			{
				cria_imovel(imovel);
			}
		});
	});
	
}


function salva_pesquisa()
{
	//seta o nome da pesquisa.
   $.ajax({type: "POST", url: "/map/SavePesquisa", data: "nomePesquisa="+$("#nome_pesquisa").val() });
}



/******************************************
* BOTOES INSERIDOS NO MAPA
********************************************/

function criar_botoes_mapa() 
{       
	criar_botao_salvar_pesquisa();
	criar_botao_desenhar_circunferencia();
	criar_botao_desenhar_poligono();

}


function criar_botao_salvar_pesquisa()
{
	var btn = document.createElement("input");
	var b = $(btn);
	
	b.attr("type", "button");	
	b.attr("value", "Salvar Pesquisa");
	b.attr("id", "btnSalvaPesquisa");
	b.addClass("btn_mapa");

	google.maps.event.addDomListener(btn, 'click', function () {
		$("#salvar_busca").show();
		$("#nome_pesquisa").focus();
		
	});
	map.controls[google.maps.ControlPosition.TOP_RIGHT].push(btn);

}

function criar_botao_desenhar_circunferencia()
{
	var btn = document.createElement("input");
	var b = $(btn);
	
	b.attr("type", "button");	
	b.attr("value", "Desenhar Raio");
	b.attr("id", "btnRaio");
	b.addClass("btn_mapa");

	google.maps.event.addDomListener(btn, 'click', function () {
		$("#btnPoliogno").val("Desenhar Poligono");
		if(b.val() == "Desenhar Raio"){
			b.val("Remover Raio");
			adicionar_busca_raio();
		} else {
			b.val("Desenhar Raio");
			remover_circunferencia();
		}
	});
	map.controls[google.maps.ControlPosition.TOP_RIGHT].push(btn);
}

function criar_botao_desenhar_poligono()
{
	var btn = document.createElement("input");
	var b = $(btn);
	
	b.attr("type", "button");	
	b.attr("value", "Desenhar Poligono");
	b.attr("id", "btnPoliogno");
	b.addClass("btn_mapa");

	google.maps.event.addDomListener(btn, 'click', function () {
		$("#btnRaio").val("Desenhar Raio");
		if(b.val() == "Desenhar Poligono"){
			b.val("Remover Poligono");
			adicionar_busca_poligono();
		} else {
			b.val("Desenhar Poligono");
			remover_poligono();
		}
	});
	
	map.controls[google.maps.ControlPosition.TOP_RIGHT].push(btn);
}