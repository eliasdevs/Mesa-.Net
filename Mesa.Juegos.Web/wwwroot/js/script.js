const typesOfCards={
  Picas:0,
  Corazones:1,
  Diamantes:2,
  Treboles:3
}
const cardValues={
  As:1,
  J:11,
  Q:12,
  K:13
}
var cartasJugador = document.getElementById("cartas-jugador");
var cartasCrupier = document.getElementById("cartas-crupier");

const palos = [typesOfCards.Corazones, typesOfCards.Diamantes, typesOfCards.Picas, typesOfCards.Treboles];
const valores = [cardValues.As, "2", "3", "4", "5", "6", "7", "8", "9", "10", cardValues.J, cardValues.Q, cardValues.K];
let baraja = [];//guarda todas las combinaciones de palos y valores

// Crea una baraja con todas las combinaciones de palos y valores
for (let palo of palos) {
  for (let valor of valores) {
    baraja.push(prepareCard(valor, palo));
  }
}
//funcion para preparar la carta
function prepareCard(value,type){
  // Crear un nuevo elemento <card-t>
  var card = document.createElement("card-t");

  // Establecer los valores de los atributos suit y rank
  card.setAttribute("suit", type);
  card.setAttribute("rank", value);
  card.setAttribute("class", "card");

  // Agregar el elemento creado al elemento padre
  //parentElement.appendChild(card);
  return card;
}

// Función para mezclar la baraja
function mezclar(baraja) {    
  for (let i = 0; i < baraja.length; i++) {
    let cartaTemporal = baraja[i];
    let indiceAleatorio = Math.floor(Math.random() * baraja.length);
    baraja[i] = baraja[indiceAleatorio];
    baraja[indiceAleatorio] = cartaTemporal;
  }
  return baraja;
}

baraja = mezclar(baraja);
console.log(baraja.length);//ya muestra 52 la cantidad de cartas
let jugador = [];
let crupier = [];
function vontarBarajas(){
    document.getElementById("barajas-pendientes").innerHTML = baraja.length; 
}
// Función para repartir las cartas
function repartirCartas() {
  cartasJugador.innerHTML="";
  cartasCrupier.innerHTML="";
  jugador.push(baraja.shift());//elimina la carta
  crupier.push(baraja.shift());
  jugador.push(baraja.shift());
  crupier.push(baraja.shift());
  mostrarCartas();
}

// Función para mostrar las cartas en el HTML
function mostrarCartas() {
    vontarBarajas();
  
  for (let i = 0; i < jugador.length; i++) {
    cartasJugador.appendChild(jugador[i]);
  }
  
  var puntajeJugador=obtenerPuntaje(jugador);
  document.getElementById("puntaje-jugador").innerHTML = puntajeJugador;

  
  for (let i = 0; i < crupier.length; i++) {
    cartasCrupier.appendChild(crupier[i])
    
  }
  var puntajeCrupier=obtenerPuntaje(crupier);
  document.getElementById("puntaje-crupier").innerHTML = puntajeCrupier;

  if(puntajeCrupier>21 || puntajeJugador>21){
    
    document.getElementById("mensaje").innerHTML = ((puntajeCrupier>21)?"Perdio el Crupier":"Perdiste");
    //todo desabilitar botones y dejar solo el de repartir cartas
  }
  if(puntajeCrupier==21 || puntajeJugador==21){
    
    document.getElementById("mensaje").innerHTML = ((puntajeCrupier==21)?"Gano el Crupier":"Has Ganado");
    //todo desabilitar botones y dejar solo el de repartir cartas
  }
}

// Función para obtener el puntaje de una mano de cartas
function obtenerPuntaje(mano) {  
  let puntaje = 0;
  let tieneAs = false;
  let conteoAs=0;
  for (let i = 0; i < mano.length; i++) {
    let valorCarta = mano[i].getAttribute('rank');    
    if (valorCarta === "1") {
      tieneAs = true;
      conteoAs++;
      puntaje += 11;
    } else if (valorCarta === "11" || valorCarta === "12" || valorCarta === "13") {
      puntaje += 10;
    } else {
      puntaje += parseInt(valorCarta);
    }
  }
  if (tieneAs && puntaje > 21) {
    puntaje -= (conteoAs*10);
  }
  return puntaje;
}

// Función para verificar si un jugador tiene blackjack
function verificarBlackjack() {
  let puntajeJugador = obtenerPuntaje(jugador);
  let puntajeCrupier = obtenerPuntaje(crupier);
  if (puntajeJugador === 21) {
    document.getElementById("mensaje").innerHTML = "¡Blackjack! ¡Ganaste!";
  } else if (puntajeCrupier === 21) {
    document.getElementById("mensaje").innerHTML = "El crupier tiene Blackjack. ¡Perdiste!";
  }
}
// Función para el turno del crupier
function turnoCrupier() {
  let puntajeCrupier = obtenerPuntaje(crupier);
  while (puntajeCrupier < 17) {
    crupier.push(baraja.shift());
    puntajeCrupier = obtenerPuntaje(crupier);
    mostrarCartas();
  }
  if (puntajeCrupier > 21) {
    document.getElementById("mensaje").innerHTML = "El crupier se pasó. ¡Ganaste!";
  } else {
    determinarGanador();
  }
}

// Función para determinar el ganador del juego
function determinarGanador() {
  let puntajeJugador = obtenerPuntaje(jugador);
  let puntajeCrupier = obtenerPuntaje(crupier);
  if (puntajeJugador > 21) {
    document.getElementById("mensaje").innerHTML = "Te pasaste de 21. ¡Perdiste!";
  } else if (puntajeCrupier > 21) {
    document.getElementById("mensaje").innerHTML = "El crupier se pasó. ¡Ganaste!";
  } else if (puntajeJugador > puntajeCrupier) {
    document.getElementById("mensaje").innerHTML = "¡Ganaste!";
  } else if (puntajeJugador === puntajeCrupier) {
    document.getElementById("mensaje").innerHTML = "Empate.";
  } else {
    document.getElementById("mensaje").innerHTML = "Perdiste.";
  }
  document.getElementById("boton-repartir").disabled = false;
  document.getElementById("boton-plantarse").style.display ="none"; 
  document.getElementById("boton-repartir").style.display ="block"; 
  document.getElementById("boton-plantarse").disabled = true;
  document.getElementById("boton-pedir-carta").disabled = true;
}

// Función para cuando el jugador pide otra carta
function pedirCarta() {
  
  jugador.push(baraja.shift());
  mostrarCartas();
  let puntajeJugador = obtenerPuntaje(jugador);
  if (puntajeJugador > 21) {
    determinarGanador();
  }
}

// Inicializa el juego

verificarBlackjack();
// Event listeners para los botones

document.getElementById("boton-repartir").addEventListener("click", function() {
  document.getElementById("boton-plantarse").style.display ="block"; 
  document.getElementById("boton-plantarse").style.marginLeft = '70%';
  document.getElementById("boton-pedir-carta").style.marginLeft = '20%';
  document.getElementById("boton-repartir").style.display = 'none';  

  baraja = mezclar(baraja);
  jugador = [];
  crupier = [];
  repartirCartas();
  document.getElementById("mensaje").innerHTML = "";
  document.getElementById("boton-repartir").disabled = true;
  document.getElementById("boton-plantarse").disabled = false;
  document.getElementById("boton-pedir-carta").disabled = false;
});

document.getElementById("boton-plantarse").addEventListener("click", function() {
  turnoCrupier();
});

document.getElementById("boton-pedir-carta").addEventListener("click", function() {
  pedirCarta();
});
