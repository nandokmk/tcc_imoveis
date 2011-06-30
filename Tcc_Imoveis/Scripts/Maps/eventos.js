
//onload da pagina
$(document).ready(function () {

	init();
	//exibir_pontos();

	//botão de busca
	$("#start_search_button").click(function() {
  	 
  });

	//botão salvar busca
 	$("#salva").click(function() {
       $.ajax({type: "POST", url: "/map/SavePolygon", data: "polygon="+creator.showData()});
   });

   $("#ok_salvar_pesquisa").click(function() {
		salva_pesquisa();
   });

   $("#start_search_button").click(function() {
		if(creator)
		{
			$("#polygon").val(creator.showData());
		}
		if(distanceWidget != null) 
		{

			//pega o raio da circunferencia
			$("#distancia").val(distanceWidget.get('distance'));

			//pega o ponto central da circunferencia
			$("#ponto_central").val(distanceWidget.get('position'));
	 
		}
		var endereco = $("#id_input_search").val();
		  if(endereco) 
		  {
				geocoder = new google.maps.Geocoder();
				geocoder.geocode({ address: endereco }, function (results, status) {
					//verifica se encontrou o endereco
				if (status == google.maps.GeocoderStatus.OK) {
							
					//centraliza o mapa no ponto buscado        
					map.fitBounds(results[0].geometry.viewport);
					
					//cria uma marcação no mapa
					cria_marker(results[0].geometry.location)


					 $("#distancia").val($("#select_distancia").val());
					 $("#ponto_central").val(results[0].geometry.location);

					 executar_busca("/Map/PesquisaTempoReal", $("#form_pesquisa_atributos").serialize());
					
				} 
					else
				 {
					alert("Endereço não encontrado");
				}
				 });


			 
		  } else {
			  executar_busca("/Map/PesquisaTempoReal", $("#form_pesquisa_atributos").serialize());
		  }
		
		/* $("#btn_buscar").click(function () {
                $.post("/Map/PesquisaTempoReal", $("#formulario_busca_geral").serialize(), 
                function(json){
				    
				    reset();
		            $.each(json, function(i, imovel){
                        //verifica se o ponto não está vazio
                        if(imovel['x'] != undefined) 
                        {
                            cria_imovel(imovel);
                        }
		        
		            });
                });

            });*/
   });

   

	criar_botoes_mapa();
});
