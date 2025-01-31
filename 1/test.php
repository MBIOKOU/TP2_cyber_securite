<?php

require_once('../_helpers/strip.php');

// https://depthsecurity.com/blog/exploitation-xml-external-entity-xxe-injection

// Désactiver le chargement d'entités externes
libxml_disable_entity_loader(true);

$xml = strlen($_GET['xml']) > 0 ? $_GET['xml'] : '<root><content>No XML found</content></root>';

$document = new DOMDocument();
//1
$document->xmlStandalone = true;

$document-> localXML($xml,LIBXML_NOENT);
// Désactiver les DTD de manière explicite
if ($document->doctype) {
    $document->doctype->removeChild($document->doctype);
}


$parsedDocument = simplexml_import_dom($document);

echo htmlspecialchars($parsedDocument->content, ENT_QUOTES, 'UTF-8');

?>
