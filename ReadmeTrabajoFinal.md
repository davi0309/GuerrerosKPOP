# Ejercicio Trabajo Final
Computación gráfica

## GIF funcionamiento y Build

[poderes VFX]([https://youtu.be/iTGMid2Y4QM](https://youtu.be/6C6ece7_0dA))

[Drive de Build.zip](https://drive.google.com/file/d/1-jfoehhyX7InHX0ZY3EP-rozRMYMI4j9/view?usp=drive_link)

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

### Partes

## Flecha


La flecha fue un modelo propia y cuenta en el proprio sistema de particylas con un sistema de partículas que hace que gire, tiene una textura hecha a raíz del mapa de UV del modelo.

<img width="1056" height="475" alt="imagen" src="https://github.com/user-attachments/assets/a90c340a-0ded-47ae-9928-b55a748e3316" />

## Humo y estrellas

El sistema de partículas del humo utiliza un flipbook para simular cómo este se va disipando progresivamente, creando la sensación de que se desvanece mientras sigue la trayectoria de la flecha. Además, cuenta con un sistema de partículas que emite elementos escalados para reforzar la sensación de velocidad y movimiento, y otro que genera pequeñas estrellas que se desplazan en la misma dirección de la flecha, complementando visualmente su recorrido.
<img width="1451" height="551" alt="imagen" src="https://github.com/user-attachments/assets/f0f9dc20-0971-48c8-a721-a49a38899ecd" />

<img width="1637" height="642" alt="imagen" src="https://github.com/user-attachments/assets/809324eb-4e4a-48d6-b16e-6c17915af10d" />


## Trails

Los trails son estos sistemas que tienen 4 diferentes colores/materiales, uno morado y su sombre y uno amarillo y su sombra, estos se crean por medio del shader.

<img width="1635" height="676" alt="imagen" src="https://github.com/user-attachments/assets/eefe2297-6428-43c4-89f0-82b4a2e96a1a" />


## Shader

El shader hace un material para el trail y maneja todo de los colores y sombras de esto.

# Zoltraak

Este VFX se trabajo la habilidad del personaje Frieren, el efecto son texturas que van cargando y van formando la imagen del hechizo, luego las del centro se contraen y carga el poder.

<img width="1072" height="526" alt="imagen" src="https://github.com/user-attachments/assets/dda796a3-e272-4d2b-8546-0919bd76cbb8" />

### Componentes

Se trabajo con diferentes partes que están subdivididas, primero es el circulo central y después el efecto del rayo:

## Circulo central

Este circulo central tiene tres capas separadas diferentes, la primera es el borde, este es el primero en crearse y cuenta por medio del shader con un barrido radial que es lo que enciende las luces del primer circulo, e exterior.


<img width="1273" height="532" alt="imagen" src="https://github.com/user-attachments/assets/334a5ac5-300e-43f8-b7ec-88f460c02739" />





Luego contamos con el centro del circulo que carga después del borde, este gira con el resto de las texturas y en un punto de el VFX se reduce para dar este efecto de carga:



<img width="1242" height="525" alt="imagen" src="https://github.com/user-attachments/assets/1c29e174-0edf-4e37-ab8b-19a8596e14fc" />



Y por ultimo los círculos del centro, estos tienen una intensidad en su color y giran con el resto, incluso se reduce y vuelven a crecer en la parte del ataque:
<img width="1227" height="517" alt="imagen" src="https://github.com/user-attachments/assets/a44209d6-da9e-4b11-8ec7-8ae39296f07f" />



## Ataque de rayo
El ataque de rayo cuenta con 2 destellos cuando dispara uno principal y otro secundario, el ataque como tal que sale del centro y los destellos que nacen del la base o núcleo del efecto giran, el destello principal es el marcado con rojo y el secundario con amarillo.

<img width="1562" height="647" alt="imagen" src="https://github.com/user-attachments/assets/5a88de0a-0a9e-4f18-bd9b-8c80522145b7" />


## Shader


Con el Shader se trabajo lo del barrido radial, que ilumina el borde exterior.

# Global Volume

Se trabajó un **Global Volume independiente para cada VFX**, ajustando la iluminación y los efectos de postprocesamiento según las necesidades visuales de cada uno.

- **Rayo:** Se buscó aumentar la intensidad visual del efecto mediante los ajustes de *Bloom* y *Color Adjustments*, resaltando su brillo y haciendo que el impacto visual fuera más llamativo.
- **Zoltraak:** Se mejoró la iluminación de las runas para darles mayor protagonismo. También se realizaron ajustes de *Color Adjustments* para intensificar los colores del efecto y transmitir una mayor sensación de magia.
- **Flecha:** Se trabajó con *Color Adjustments*, *Bloom* y *Vignette* para mejorar la apariencia general del entorno, resaltar los elementos visuales del efecto y darle mayor profundidad a la escena.
















