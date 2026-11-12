# Ejercicio Trabajo Final
Computación gráfica

## GIF funcionamiento 

[poderes VFX](https://youtu.be/iTGMid2Y4QM)

## Rayo de poder 

<img width="1562" height="642" alt="imagen" src="https://github.com/user-attachments/assets/9ac801c1-bea4-423c-adf9-1b9a11fe4b96" />

Efecto de rayo de poder la idea fue que ll personaje concentra energía en las manos y la libera como un rayo largo, con un núcleo brillante, una capa de electricidad y chispas amarillas alrededor.

### Componentes

Para este VFX se trabajo en varias partes, que es el nucleo y el rayo.

## Nucleo

<img width="1557" height="640" alt="imagen" src="https://github.com/user-attachments/assets/2022f087-e443-421d-8cc3-5595d30026dd" />

En el nucleo se trabajaron dos sistemas de particulas, el primero marcado con rojo es un quad, siempre mira a la cámara, con brillo HDR y blending Additive ademas que tiene un scale random que hace que se vea inestable ya que crece y se vuelve pequeño. Representa la energía concentrada antes de salir. Tiene la Sorting Priority más alta para dibujarse encima del rayo. 

El otro efecto son partículas amarillas alargadas que salen desde el centro hacia todas las direcciones, como una pequeña explosión. Nacen dentro de una esfera pequeña, salen con velocidad esférica, frenan con Linear Drag y viven poco tiempo ademas que estan hechos de esta manera para representar tambien el poder que sale de las manos del mago.

## Rayo

<img width="1560" height="650" alt="imagen" src="https://github.com/user-attachments/assets/1cd93c4a-506a-461e-9845-d0a56f4d4fd3" />


El rayo cuanta con 4 efectos que le dan ese efecto y estos son:

- El cuerpo principal o la base del rayo es lo marcado verde y es el haz cian brillante con la textura hecha a mano desplazándose a lo largo del rayo.

- la sombra es una copia del rayo con blending Alpha y esta marcado en morado, tiene un color oscuro tirando a morado, debajo de la base sse hizo para darle contraste y profundidad al brillo.

- el tercer efecto es el marcado en naranja y este es el que le da un efecto doble como se otra linea es una segunda capa de energía con la distorsión (ruido) más alta, para que se vea inestable y eléctrica.

- Y por ultimo esta lo marcado en rojo que son estas íneas amarillas finas que viajan a lo largo del rayo y le dan sensación de velocidad, son un conjunto de particulas circulares escaladas de manera para que queden delgadas.




### Imagen 1 — Efecto del slash de fuego
Compuesto por:

* Main que da su forma y color principal  
* Contraste más oscuro para resaltar las partes brillantes
* Shine para dar la luz que tiene el fuego
* Hit para indicar la colisión con algo  


### Imagen 2 — Distorsión post slash
* Se difumina de manera irregular el fuego
* Una distorsión 

## Segundo Ataque ( seg 1:72 ) 

Para este segundo movimiento decidimos explorar un efecto de agua para el slash, caracterizado por tonos azules/blancos asimilando la espuma del agua.

<img width="1245" height="728" alt="image" src="https://github.com/user-attachments/assets/c736ac1c-ebcc-4c6f-9f5e-8de2aaf9d17f" />

<img width="1197" height="739" alt="image" src="https://github.com/user-attachments/assets/3a438df5-1b72-48ab-9142-255ddfe6c276" />



### Imagen 1 - El efecto del agua

### Imagen 2 - Hit

Aquí volvemos a indicar el choque de nuestro ataque contra algun objeto haciendo que el agua salpique.


## Tercer Ataque ( seg 2:25 ) 

Y finalmente decidimos explorar algo más creativo fuera de los elementos clásicos de fuego, agua, tierra, viento; decidimos explorar la manera de hacer un ataque de galaxia, nos imaginamos que debería tener muchos tonos morados, negros y blancos. También incorporamos unas estrellas para reforzar el feeling espacial de el ataque.


<img width="857" height="679" alt="image" src="https://github.com/user-attachments/assets/84300fa9-e811-4fcc-9b8f-4d374f7d5539" />
<img width="1245" height="716" alt="image" src="https://github.com/user-attachments/assets/85f6747c-bc39-4d3a-9b3c-1b896fffd689" />




### Imagen 1 - Ataque espacial

* Efectos de estrella  
* Contrastes negros más oscuros para reforzar el senimiento de vacío del espacio
* Destellos como luces de las estrellas









