PPCpuMon
====

Party Parrot のCPUモニタです。

[![Party Parrot CPU monitor](https://img.youtube.com/vi/9o4XuVESALk/0.jpg)](https://www.youtube.com/watch?v=9o4XuVESALk)

## 使い方

タスクバーアイコンを右クリックするとメニューが表示されます。

![メニュー](img/img2.png)

#### CPU

CPUの表示方法を選択します。

* Total：全てのコアを1つのアイコンで表示
* Physical Core：物理コアごとに表示
* Logical Core：論理コアごとに表示

Physical Core を選択した場合は、物理コアのすべてのコアの平均値となります。

#### Animation speed

アニメーションの速度を設定します。

#### Stop animation

CPU使用率が設定値以下になったときにアニメーションを停止します。
「None」を選択した場合は停止しません。

#### CPU sampling Interval

CPU使用率のサンプリング間隔を設定します。

#### Exit

アプリケーションを終了します。

## 動作環境

* Microsoft Windows 7 以降
* Microsoft .net Framework 3.6 以降

## インストール方法

[PPCpuMonInstaller.msi](https://github.com/tomozh/PPCpuMon/blob/master/PPCpuMonInstaller/Release/PPCpuMonInstaller.msi?raw=true) を実行してインストール、または、[PPCpuMon.exe](https://github.com/tomozh/PPCpuMon/blob/master/bin/Release/PPCpuMon.exe?raw=true) を、適当なフォルダに入れて実行して下さい。

実行後は、タスクトレイに常駐します。

インストーラでインストールした場合は、スタートアップへ自動的に登録されます。(ログオン時に自動起動します)

## トラブルシューティング

### 「エラー：無効なインデックス '' がレジストリから読み取られたため、カウンター名データを読み込むことができません。」というエラーが出て起動しない

パフォーマンスカウンタが破損している可能性があります。以下の手順に従って操作を行ってから、アプリケーションを起動してみて下さい。

1. コマンドプロンプト、または Windows PowerShell を管理者モードで起動する。(例：スタートボタンを右クリックして「Windows PowerShell (管理者)」を実行)
2. 以下のコマンドを実行する。
~~~
cd C:\Windows\System32
lodctr /R
cd C:\Windows\SysWOW64
lodctr /R
~~~
 
## ライセンス

[MIT](https://opensource.org/licenses/mit-license.php)

## 著作者

[tomozh](http://ore-kb.net)
