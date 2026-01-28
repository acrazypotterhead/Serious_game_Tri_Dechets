# Introduction

[![Demo video](https://drive.usercontent.google.com/download?id=1n5Iy89U6EJFdt9r1kt6B0HHqYPeNxMtu&authuser=0)](https://youtu.be/COlwkDRjTm8?si=foIVOxPAoi9dKHJO)

Lab-Secure VR est une application de réalité virtuelle sérieuse conçue pour la formation des techniciens et des étudiants en chimie. Le projet simule un environnement de laboratoire réaliste où l'apprenant doit identifier, analyser et trier des déchets chimiques tout en gérant des incidents de sécurité imprévus.

## Index

- [About](#about)
- [Usage](#usage)
  - [Installation](#installation)
  - [Commands](#commands)
- [Development](#development)
  - [Pre-Requisites](#pre-requisites)
  - [Development Environment](#development-environment)
  - [File Structure](#file-structure)
  - [Build](#build)  
  - [Deployment](#deployment)  
- [Credit/Acknowledgment](#creditacknowledgment)
- [License](#license)

## About

Le projet Lab-Secure VR répond à une double problématique : la sécurité des personnels de laboratoire (SST) et la responsabilité environnementale (RSE). L'application plonge l'utilisateur dans une boucle de travail où la vigilance est la clé. 



Contrairement à une simulation linéaire, Lab-Secure VR intègre un système d'événements aléatoires simulant des fuites ou des projections chimiques. L'apprenant doit valider trois indicateurs de performance en fin de session : la conformité technique du tri, la sécurité comportementale (port des EPI, réactivité aux incidents) et l'impact écologique (ODD 12).

## Usage

L'application est destinée à être utilisée avec un casque de réalité virtuelle autonome (Meta Quest) ou une configuration PCVR.

### Installation

1. **Préparation du matériel** : Assurez-vous que votre casque Meta Quest est en mode développeur.
2. **Récupération de l'exécutable** : Téléchargez le fichier `.apk` (pour Quest) ou le dossier build (pour PCVR) depuis la section Releases du dépôt.
3. **Installation sur Quest** :
   - Utilisez SideQuest ou Meta Quest Developer Hub.
   - Faites glisser le fichier `.apk` dans l'interface de l'outil pour l'installer.
4. **Lancement** : Sur le casque, allez dans la bibliothèque d'applications, section "Sources inconnues", et lancez Lab-Secure VR.

```bash
$ adb install lab-secure-vr-final.apk
```

### Commands

- **Saisie d'objet** : Gâchette latérale (Grip).
- **Activation de douche/bouton** : Gâchette d'index (Trigger) ou contact physique avec le bouton/plateforme de douche.
- **Déplacement** : Joystick gauche (mouvement), joystick droit (tourner la tête) ou mouvement physique (Room Scale).

## Development

Cette section explique comment configurer l'environnement pour développer sur le projet.

### Pre-Requisites

- Unity Hub.
- Unity Editor version **6.0.62f1**.
- Android Build Support (pour Meta Quest).
- XR Plugin Management et XR Interaction Toolkit.
- Système Addressables installé via le Package Manager.

### Development Environment

1. **Clonage du projet** :
```bash
$ git clone https://github.com/acrazypotterhead/Serious_game_Tri_Dechets.git
```
2. **Configuration Unity** :  
  Ouvrez Unity Hub et ajoutez le dossier du projet. Assurez-vous d'utiliser la version 6.0.62f1.  
3. **Lancement de la Scène** :  
  Ouvrez la scène située dans Assets/Scenes/SceneWithAllInteraction.unity. C'est le point d'entrée unique du projet.  
4. **Initialisation des Addressables** :  
  Allez dans Window > Asset Management > Addressables > Groups. Si la fenêtre est vide ou si les groupes manquent, effectuez un build des assets (voir section Build).  

### File Structure
```text
.
├── Assets
│   ├── Materials           # PBR Materials
│   ├── Models              # Raw 3D files (FBX/OBJ)
│   ├── Prefabs             # Objets 3D (Lab, Socle, chemset -MQ/-PCVR)
│   ├── Scenes              # Scènes du projet
│   ├── Scripts             # Logique C# (Tris, Incidents, Addressables)
│   ├── Settings            # Configuration URP et XR
│   ├── Sounds              # Effets sonores et UI
│   └── Textures            # Textures sources
├── Packages                # Dépendances Unity (XRI, Addressables...)
├── ProjectSettings         # Paramètres moteur (Input, Tags, Graphics)
└── README.md
```
### Build

Pour générer l'application, vous devez obligatoirement suivre cet ordre pour que les assets soient inclus :

1. **Build des Addressables** : Dans la fenêtre `Addressables Groups`, cliquez sur `Build > New Build > Default Build Script`.
2. **Build du Projet** : Allez dans `File > Build Settings`. Assurez-vous que la scène `SceneWithAllInteraction` est cochée et en index 0.
   - **Pour Meta Quest** : Sélectionnez **Android** et cliquez sur `Build`.
   - **Pour PC** : Sélectionnez **Windows** et cliquez sur `Build`.

### Deployment

Le déploiement pour le jury s'effectue via le transfert du fichier `.apk` final (voir releases). Pour les tests internes, utilisez le bouton **"Build and Run"** dans Unity avec le casque branché en USB-C.

## Credit/Acknowledgment

- [Alexandre Baudin](https://github.com/Antiflex)
- [Roxane Braconnier](https://github.com/R0xane)
- [Yoke Ngassa](https://github.com/Rolwenx)
- [Jessica Rasoamanana](https://github.com/acrazypotterhead)
