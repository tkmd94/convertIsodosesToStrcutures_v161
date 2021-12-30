# convertIsodosesToStrcutures_v161

[![LicenseBadges](https://badges.frapsoft.com/os/mit/mit.svg?v=102)](https://github.com/ellerbrock/open-source-badge/)  

VARIAN社製治療計画装置EclipseのScripting codeです。

実装機能：表示している全てのIsodose lineを輪郭に変換するスクリプト。
　　　　　

注）Isodose Levelの一覧に登録していても、線量域がないIsodose lineは輪郭に変換されません。
     
注）元に戻したい場合は「Undo」処理をしてください。

注）本スクリプトはWritableにつき、データに変更を加えますので、ご注意ください。
     
作成環境：v16.1

動作検証：v16.1

## リリースノート

### version 1.1.0.1
v16.1より、作成した輪郭の色をIsodose lineに合わせる機能を追加。
