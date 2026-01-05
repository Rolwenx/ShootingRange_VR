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

## Auteur
- Yoke NGASSA
- Promotion 2026
