# Ejercicio Trabajo Final
Computación gráfica

## GIF funcionamiento 

[poderes VFX](https://youtu.be/iTGMid2Y4QM)

# Rayo de poder 

<img width="1562" height="642" alt="imagen" src="https://github.com/user-attachments/assets/9ac801c1-bea4-423c-adf9-1b9a11fe4b96" />

Efecto de rayo de poder la idea fue que el personaje concentra energía en las manos y la libera como un rayo largo, con un núcleo brillante, una capa de electricidad y chispas amarillas alrededor.

### Componentes

Para este VFX se trabajo en varias partes, que es el nucleo y el rayo.

## Nucleo

<img width="1557" height="640" alt="imagen" src="https://github.com/user-attachments/assets/2022f087-e443-421d-8cc3-5595d30026dd" />

En el nucleo se trabajaron dos sistemas de particulas, el primero marcado con rojo es un quad, siempre mira a la cámara, con brillo HDR y blending Additive ademas que tiene un scale random que hace que se vea inestable ya que crece y se vuelve pequeño. Representa la energía concentrada antes de salir. Tiene la Sorting Priority más alta para dibujarse encima del rayo. 

El otro efecto son partículas amarillas alargadas que salen desde el centro hacia todas las direcciones, como una pequeña explosión. Nacen dentro de una esfera pequeña, salen con velocidad esférica, frenan con Linear Drag y viven poco tiempo ademas que estan hechos de esta manera para representar tambien el poder que sale de las manos del mago.

## Rayo

<img width="1560" height="650" alt="imagen" src="https://github.com/user-attachments/assets/1cd93c4a-506a-461e-9845-d0a56f4d4fd3" />


El rayo cuanta con 4 efectos que le dan ese efecto y estos son:

- El cuerpo principal o la base del rayo es lo marcado verde y es el haz cían brillante con la textura hecha a mano desplazándose a lo largo del rayo.

- la sombra es una copia del rayo con blending Alpha y esta marcado en morado, tiene un color oscuro tirando a morado, debajo de la base se hizo para darle contraste y profundidad al brillo.

- el tercer efecto es el marcado en naranja y este es el que le da un efecto doble como se otra linea es una segunda capa de energía con la distorsión (ruido) más alta, para que se vea inestable y eléctrica.

- Y por ultimo esta lo marcado en rojo que son estas líneas amarillas finas que viajan a lo largo del rayo y le dan sensación de velocidad, son un conjunto de partículas circulares escaladas de manera para que queden delgadas.

## Shader

En el shader de este VFX lo usan todas las capas del rayo. Cada output del VFX ajusta sus propios valores, así que un mismo shader sirve para la base, la sombra y el rayo de poder, este maneja lo que es el movimiento de la textura para darle ese efecto de rayo y también maneja la distorsion y alpha de este efecto.

# Ashe Guardiana Estelar


El efecto de Flecha fue pensado como la habilidad definitiva de Ashe de LOL, con tematica de guardiana estelar, este efecto es la flecha, con un estela que deja con efectos de estrella, el modelo y las texturas se trabajaron a mano.

<img width="1303" height="528" alt="imagen" src="https://github.com/user-attachments/assets/e3eab48d-511f-477c-889c-dc411e8a93e8" />

### Componentes

# Zoltraak

Este VFX se trabajo la habilidad del personaje Frieren, el efecto son texturas que van cargando y van formando la imagen del hechizo, luego las del centro se contraen y carga el poder.

<img width="1072" height="526" alt="imagen" src="https://github.com/user-attachments/assets/dda796a3-e272-4d2b-8546-0919bd76cbb8" />

### Componentes

Se trabajo con diferentes partes que están subdivididas, primero es el circulo central y después el efecto del rayo:

## Circulo central

Este circulo central tiene tres capas separadas diferentes, la primera es el borde, este es el primero en crearse y cuenta por medio del shader con un barrido radial que es lo que enciende las luces del primer circulo, e exterior.
<img width="405" height="463" alt="imagen" src="https://github.com/user-attachments/assets/922db072-bd21-4ef8-aa2a-df3823704693" />




Luego contamos con el centro del circulo que carga después del borde, este gira con el resto de las texturas y en un punto de el VFX se reduce para dar este efecto de carga:

<img width="411" height="457" alt="imagen" src="https://github.com/user-attachments/assets/66880143-2665-4bc3-a4a6-5cd7f5a6d366" />











