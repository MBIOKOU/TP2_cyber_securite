<?php

require_once('../_helpers/strip.php');

libxml_disable_entity_loader (true);

$xmlfile = file_get_contents('php://input');
$dom = new DOMDocument();
$dom->loadXML($xmlfile, LIBXML_NOENT | LIBXML_NOCDATA | LIBXML_NONET);
$info = simplexml_import_dom($dom);
$name = $info->name;
$tel = $info->tel;
$email = $info->email;
$password = $info->password;

echo "Sorry, " . htmlspecialchars($info->email, ENT_QUOTES, 'UTF-8') . " is already registered!";
?>
