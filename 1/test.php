<?php

require_once('../_helpers/strip.php');

// https://depthsecurity.com/blog/exploitation-xml-external-entity-xxe-injection

libxml_disable_entity_loader (true);  // Désactiver le chargement 

$xml = strlen($_GET['xml']) > 0 ? $_GET['xml'] : '<root><content>No XML found</content></root>';

$document = new DOMDocument();
$document->loadXML($xml, LIBXML_NOENT | LIBXML_NONET);
$parsedDocument = simplexml_import_dom($document);

echo htmlspecialchars($parsedDocument->content, ENT_QUOTES, 'UTF-8');
