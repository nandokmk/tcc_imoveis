
//onload da pagina
$(document).ready(function () {

	init();
	exibir_pontos();

	//botão de busca
	$("#start_search_button").click(function() {
  		var endereco = $("#id_input_search").val();
      if(endereco) 
      {
          centralizar_mapa(endereco);
      }
  });

	//botão salvar busca
 	$("#salva").click(function() {
       $.ajax({type: "POST", url: "/map/SavePolygon", data: "polygon="+creator.showData()});
   });

   $("#ok_salvar_pesquisa").click(function() {
		salva_pesquisa();
   });

	criar_botoes_mapa();
});
