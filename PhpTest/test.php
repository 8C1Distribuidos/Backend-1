<?php
    $url='https://localhost:5001/api/Product/GetId?id=3';
    $json = file_get_contents($url);
    $json = json_decode($json);
    $id = $json['id'];
    $name = $json['name'];
    echo "id: ";
    echo $id;
    echo "<br>";
    echo "name: ";
    echo $name;
?>