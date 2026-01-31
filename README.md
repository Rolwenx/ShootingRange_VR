# TP3 — Shaders de guidage & interaction

## Fonctionnalités

### A — Conversion URP
- Conversion du projet et du décor en **Universal Render Pipeline** (voir dans Assets, sous le nom URP_MainPipeline & URP_MainRenderer)
- Scène fonctionnelle et stable en VR (pas de textures roses)

### B — Guidage par occlusion
- **Contour visible uniquement lorsque la zone "objectif" est caché**
- L'utilisateur arrive dans la scène et peut voir les contours de la zone objectif, lorsque celle-ci est cachée par d'autres objets. Lorsque la zone est visible, il y a un peu de contours visibles, mais négligeable
- Ajout d’un pictogramme de point d'exclamation au-dessus de l’objectif, lui aussi occluded-only

*Avec le shader graph "SG_OccludedOutline_Target" (dossier Shader Graphs), associé au matériau M_OccludedOutline_Target (dossier Shader Graphs)*

### C — Feedback interactif (arme)
- Shader Unlit de feedback visuel :
  - Hover : surbrillance + rim léger (couleur jaune)
  - Grab : changement de couleur + pulse d’émission (couleur verte)
(Effet de pulsion)
- Pilotage par le script **MaterialPropertyBlock** (dossier script)

*Avec le shader graph "SG_InteractHighlight_Weapon" (dossier Shader Graphs), associé au matériau M_InteractHighlight_Weapon (dossier Shader Graphs)*

---

# TP4 — Incarnation humanoïde “incarnation-ready” (XR)

## Fonctionnalités

### A — Personnage humanoïde et locomotion
- Import d’un **personnage humanoïde** avec Avatar valide.
- Mise en place d’un **Blend Tree de locomotion** (Idle / Walk / Run) piloté par une **variable cible** issue des déplacements du personnage VR (statique → Idle, marche → Walk, course → Run).


### B — Incarnation VR simulée par IK 
- Le personnage PNJ représente le **corps incarné VR**.
- Les mouvements du joueur en VR pilotent des **targets** (tête et mains).
- Le corps du personnage suit ces targets via **Animation Rigging** :
  - Two-Bone IK pour les bras (mains droite et gauche),
  - Multi-Parent Constraint sur la tête afin de suivre l’orientation du casque VR.
- Le personnage se déplace en VR, et le **corps suit naturellement la pose du joueur**.


### D — Cohérence avec le TP3
- Réutilisation du **shader hover / grab** de l’arme. L'utilisateur attrape l'arme et celui-ci se place dans la main du personnage PNJ incarné.
- Une animation Equip est utilisé, mais au vu du Rigging, elle n'est pas visible. L'arme s'équipe à un moment de l'animation (controlé par un Animation Event)

---

# TP5 — Drone de guidage intelligent (RB-3DCP)

## Fonctionnalités

### A — Suivi du joueur
- Mise en place d’un **drone autonome** suivant le joueur en 3D à partir d’un point d’ancrage (*FollowAnchor*).
- Le déplacement est entièrement géré par script (pas de NavMesh), afin de rester compatible avec un environnement VR dynamique.

### B — Poursuite contrainte par rayons (RB-3DCP)

Implémentation d’un algorithme de **Ray-Based 3D Constrained Pursuit**, structuré en plusieurs étapes :

- **B1 — Génération des directions candidates**
Génération de directions candidates réparties dans un cône autour de la direction vers le FollowAnchor.
- **B2 — Faisabilité par contrainte dure**
Filtrage des directions candidates par SphereCast afin d’éliminer toute trajectoire menant à une collision.
- **B3 — Scoring multi-critères**
Sélection de la meilleure direction via un scoring combinant suivi de la cible, stabilité dynamique et sécurité locale.
- **B4 — Stabilisation et anti-jitter**
Application d’une hystérésis et d’un lissage temporel pour éviter les oscillations et garantir un mouvement fluide.
- **B5 — Fallback en cas de blocage**
Passage en mode Cautious avec réduction de vitesse et hover temporaire lorsqu’aucune direction faisable n’est disponible.

### C — Maintien de la distance cible
- Ajout d’une **contrainte de poursuite spatiale** permettant au drone de maintenir une distance cible avec le joueur.
- Une force de rappel vers le FollowAnchor empêche le drone de dériver ou de continuer indéfiniment dans une direction donnée.

### D — Feedback visuel d’état
- Le drone change d’apparence selon son état :
  - **Normal** : comportement standard.
  - **Cautious** : activation d’un feedback visuel (couleur jaune).

---

## Auteur
- Yoke NGASSA
- Promotion 2026
