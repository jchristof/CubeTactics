<?xml version="1.0" encoding="UTF-8"?>
<tileset name="tilemap" tilewidth="64" tileheight="64">
 <image source="tilemap.bmp" width="512" height="512"/>
 <tile id="8">
  <properties>
   <property name="type" value="trigger"/>
   <property name="value" value="spawn"/>
  </properties>
 </tile>
 <tile id="9">
  <properties>
   <property name="type" value="trigger"/>
   <property name="value" value="goal"/>
  </properties>
 </tile>
 <tile id="11">
  <properties>
   <property name="type" value="terrain"/>
   <property name="value" value="grass"/>
  </properties>
 </tile>
 <tile id="13">
  <properties>
   <property name="type" value="number"/>
   <property name="value" value="1"/>
  </properties>
 </tile>
 <tile id="14">
  <properties>
   <property name="type" value="number"/>
   <property name="value" value="2"/>
  </properties>
 </tile>
 <tile id="15">
  <properties>
   <property name="type" value="number"/>
   <property name="value" value="3"/>
  </properties>
 </tile>
 <tile id="16">
  <properties>
   <property name="type" value="number"/>
   <property name="value" value="4"/>
  </properties>
 </tile>
  <tile id="27">
  <properties>
   <property name="type" value="trigger"/>
   <property name="value" value="pressureplate"/>
  </properties>
 </tile>
  <tile id="28">
  <properties>
   <property name="type" value="portal"/>
   <property name="value" value=""/>
  </properties>
 </tile>
  <tile id="30">
  <properties>
   <property name="type" value="wall"/>
   <property name="value" value="block"/>
  </properties>
 </tile>
</tileset>
