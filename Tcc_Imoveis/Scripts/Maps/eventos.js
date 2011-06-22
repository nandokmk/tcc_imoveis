
//onload da pagina
$(document).ready(function () {

	init();

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

	criar_botoes_mapa();
});
