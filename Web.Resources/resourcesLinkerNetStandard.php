<?php

$xmlInputFile = "Web.Resources.csproj";
$xmlOutputFile = "Web.Resources.csproj";
$dynamicResourcesRootDirectory = "Frontend";

$currentDynamicEmbeddedFiles = array();
$rootDir = getcwd()."\\";

foreach(getDirContents($dynamicResourcesRootDirectory ) as $fileContent) {
	if(!is_dir($fileContent)) {
			array_push($currentDynamicEmbeddedFiles, substr($fileContent, strlen($rootDir)));
	}
}

$xml = new SimpleXMLElement(file_get_contents($xmlInputFile));              
$xml->registerXPathNamespace("base", "");
$insertNodeN = $xml->xpath("/Project/ItemGroup[None]");                          
$resourceElemsN = $xml->xpath("/Project/ItemGroup/None");
$insertNodeE = $xml->xpath("/Project/ItemGroup[EmbeddedResource]");                            
$resourceElemsE = $xml->xpath("/Project/ItemGroup/EmbeddedResource");
$hasEmbeddedResources = false;

if(is_array($resourceElemsE)) {
	if(count($resourceElemsE) > 0) {
		$hasEmbeddedResources = true;
	}
}

if($hasEmbeddedResources) {
	// delete embedded elements which path begins with $dynamicResourcesRootDirectory
	for ($i = 0; $i < count($resourceElemsN); $i++) {
		$removePath = $resourceElemsN[$i]->attributes()["Remove"];
		if(strlen($dynamicResourcesRootDirectory) < strlen($removePath) && 
			substr($removePath, 0, strlen($dynamicResourcesRootDirectory) + 1) == $dynamicResourcesRootDirectory."\\")  {
			$domRef = dom_import_simplexml($resourceElemsN[$i]);
			$domRef->parentNode->removeChild($domRef);
		}
	}
	
	for ($i = 0; $i < count($resourceElemsE); $i++) {
		$includePath = $resourceElemsE[$i]->attributes()["Include"];
		if(strlen($dynamicResourcesRootDirectory) < strlen($includePath) && 
			substr($includePath, 0, strlen($dynamicResourcesRootDirectory) + 1) == $dynamicResourcesRootDirectory."\\")  {
			$domRef = dom_import_simplexml($resourceElemsE[$i]);
			$domRef->parentNode->removeChild($domRef);
		}
	}

	// insert embedded elements which founds at $dynamicResourcesRootDirectory
	foreach($currentDynamicEmbeddedFiles as $cef) {
		$embeddedElemN = $insertNodeN[0]->addChild("None");
		$embeddedElemN->addAttribute("Remove", $cef);
		$embeddedElemE = $insertNodeE[0]->addChild("EmbeddedResource");
		$embeddedElemE->addAttribute("Include", $cef);
	}
	
} else {	
	// added new node ItemGroup for embedded elements which founds at $dynamicResourcesRootDirectory
	$projectNode = $xml->xpath("/Project");

	$newElementN = simplexml_load_string("<ItemGroup></ItemGroup>");
	
	foreach($currentDynamicEmbeddedFiles as $cef) {
		$currentChild = $newElementN->addChild('None','');
		$currentChild->addAttribute('Remove', $cef);
	}
	
	$newElementE = simplexml_load_string("<ItemGroup></ItemGroup>");
	
	foreach($currentDynamicEmbeddedFiles as $cef) {
		$currentChild = $newElementE->addChild('EmbeddedResource','');
		$currentChild->addAttribute('Include', $cef);
	}
	
	xml_adopt($projectNode[0], $newElementN);
	xml_adopt($projectNode[0], $newElementE);
}

$xml->asXML($xmlOutputFile);

// -------------------------------

function getDirContents($dir, &$results = array()) {
    $files = scandir($dir);

    foreach ($files as $key => $value) {
        $path = realpath($dir . DIRECTORY_SEPARATOR . $value);
        if (!is_dir($path)) {
            $results[] = $path;
        } else if ($value != "." && $value != "..") {
            getDirContents($path, $results);
            $results[] = $path;
        }
    }
    return $results;
}

// -------------------------------

function xml_adopt($root, $new) {
    $node = $root->addChild($new->getName(), (string) $new);
    foreach($new->attributes() as $attr => $value) {
        $node->addAttribute($attr, $value);
    }
    foreach($new->children() as $ch) {
        xml_adopt($node, $ch);
    }
}
?>