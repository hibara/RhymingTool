# ライミング・ツール

## 概要

日本語による歌詞作成支援、ライミング作成支援を目的とした、ライミング検索ツールです。

入力した文章を形態素解析して品詞に分解、それぞれの単語の「品詞」と「母音」をキーにして辞書を検索し、一致率が高いものからピックアップします。

Windowsアプリケーションで、Windows 10, 11 で動作します。.NET Framework 4.8 を使って開発しています。

## ダウンロード＆インストール

以下のページから、ダウンロードしてください。

<https://github.com/hibara/RymingTool/releases>

- ZIPファイルの場合（`RhymingTool[バージョン番号].zip`）
  中身のファイルを動かさずに実行ファイルを起動してください。
  また、アクセスに管理者権限の必要な解凍場所によっては正常に動かないことがあります。
  その辺りが、心配な方は、インストーラーを利用してください。

- インストーラーからの場合（`RhymingTool[バージョン番号].exe`）
  必要なデータ配置が心配な場合は、こちらのインストール版を
  `Program Files(x86)`にインストールされ、データベースファイルは、
  `C:\Users\[ユーザー名]\AppData\Local\RhymingTool`に作成されます。また、MeCab.DotNetが使う`dic`という辞書もここに自動生成されます。

## 使い方

いたってシンプルです。上の入力専用テキストボックスにライミングをしたい文章を入れて、「ライミング検索」ボタンを押すだけです。
検索時間がしばらくあった後に、下のテキストボックスに、結果が表示されます。

あんまり文章が巨大だと、それだけ検索に時間がかかります。一小節くらいの文章を入力して、その速度を見てから、増やすなど判断してください。
途中キャンセル機能は入れてませんが、非同期で検索するので、止めたい場合はアプリケーションを終了させてしまっても問題ありません。

また、どのように形態素解析をしているかを見るために、「形態素解析」ボタンも付けました。
こちらは辞書ファイルを検索しない分、高速に表示されるはずです。

## 動作設定

- 検索語の一致率
  パーセンテージで指定します。初期値は、「75」％です。４文字の単語なら、３文字以上が一致したものをピックアップします。

- 母音に一致する検索結果件数
  母音の後方一致した検索結果をいくつピックアップするかを指定します。初期値は「20」。
  あまり多いと検索に時間がかかるので注意してください。

- 発音と一致する検索結果件数
  母音ではなく、発音（全角カタカナ）で一致する検索結果をいくつピックアップするかを指定します。初期値は「0」です。
  一応、今回のツールは、「母音」による検索がメインですが、発音とずばり一致するものも検索できるようにしました。
  「母音」検索と一緒に、少しだけ発音の一致も探したいときは、「0」以上にしてみてください。

## 辞書のインポート

「辞書管理」タブから、インポートページへ行けます。

《キャプチャー画像》

カンマ区切りの書式でCSVファイル形式であることが条件です。

必須項目は、「単語」「品詞」「読み方（全角カタカナ）」「発音（全角カタカナ）」で並べます。

CSVの解析方法は、まず一番目に来る項目を「単語」  
次に、各項目の後ろの一文字に「～詞」が含まれれば「品詞」  
最後に全角カタカナを探しに行って、先に見つかったものが「読み方」  
そして、連続して次の項目を「発音」  
と、認識します。

一応、投入辞書の文字エンコーディングは自動判定されますが、`EUC-JP`、`Shift-JIS`、`UTF-8`が無難でしょう。

また、重複チェックも行っていますので、同じ単語は登録できない（登録をスキップする）仕様になっています。
とりあえずデータベースが壊れるのを防ぐために、「dictionary.db」は別の場所へコピーして置くことをお勧めします。

## オープンソース

MITライセンスです。

ただし、このアプリケーションの動作に必要なライブラリ群（形態素解析用、データベース操作、文字エンコーディングなど）は、
それぞれのライセンスに準じてください（GPLだったり、LGPLなどあります）。

Copyright (C) 2021 M.Hibara

## サポート、または連絡先

バグの報告、要望などあれば、お気軽に以下までご一報ください。

<https://github.com/hibara/RymingTool/issues>

> Mitsuhiro Hibara
> [m@hibara.org](mailto:m@hibara.org)