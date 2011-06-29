
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

   $("#btn_buscar").click(function() {

		executar_busca("/Map/PesquisaTempoReal", $("#form_pesquisa_atributos").serialize());
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
