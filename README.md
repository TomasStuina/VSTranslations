# VSTranslations

![GitHub release (latest SemVer)](https://img.shields.io/github/v/release/TomasStuina/VSTranslations?display_name=tag&sort=semver&style=plastic)
[![CI](https://github.com/TomasStuina/VSTranslations/actions/workflows/ci.yml/badge.svg)](https://github.com/TomasStuina/VSTranslations/actions/workflows/ci.yml)
[![CD](https://github.com/TomasStuina/VSTranslations/actions/workflows/cd.yml/badge.svg)](https://github.com/TomasStuina/VSTranslations/actions/workflows/cd.yml)
[![Coverage](https://tomasstuina.github.io/VSTranslations/badge_combined.svg)](https://tomasstuina.github.io/VSTranslations/)


## Overview
VSTranslations is a Visual Studio extension for translating code/text written in foreign languages. Supports multiple translation engines.

## Supported Visual Studio Versions

- Visual Studio 2022 (17.x)

## Quick Start
Download the latest [VSTranslations.vsix](https://github.com/TomasStuina/VSTranslations/releases/latest/download/VSTranslations.vsix) core extension and install using VSIX installer. 

![Install](./media/install.png)

> **IMPORTANT!:** VSTranslations is only the core extension. Addtional engine plugin is required for it to properly work (e.g., Google Translate plugin): 
>
>![Installed-Plugins](./media/installed-plugins.png)

## Translation Plugins
- [Google Translate](https://github.com/TomasStuina/VSTranslations/releases/latest/download/VSTranslations.Plugin.GoogleTranslate.vsix)

## Usage

Configure the default translation engine to use (e.g., Google Translate):

![Configure](./media/settings.png)

Enable **VSTranslations** toolbar:

![Configure](./media/toolbar-context.png)

Select source and target languages:

![](./media/language-select.png)

Select text you want to translate:

![Selected-Text](./media/selected.png)

Right-Click to open the context menu and click **Translate...**:

![Context-Translate](./media/translate-context.png)

The translated text is displayed as a text adornment next to the corresponding line:

![Translated](./media/translated.png)

Selecting the translated text and clicking **Delete Translation** will remove the corresponding translation addornment:

![Delete-Translation](./media/delete-translation.png)

If text is not selected then only that particular line where the cursor is placed will be translated:

![Delete-Translation](./media/cursor-placed.png)

## Notes:

The extension was something that was needed for my daily use, specially in a code base where the written language primarly neither was english, nor my native one. Since I just needed a simple solution to translate foreign text it is not exactly stable, thus there are many potential bugs I am (not) aware of.