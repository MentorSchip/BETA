using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;

public class SpawnOnMap : MonoBehaviour
{
	[SerializeField] AbstractMap _map;
	[SerializeField] LocationData testHunt;
	bool demoInitialized;

	[Geocode] List<ScholarshipSet> availableScholarships = new List<ScholarshipSet>();
	UserData currentUser { get { return PersistentDataManager.instance.currentUser; } }
	ScholarshipSet currentScholarship { get { return currentUser.currentScholarship; } }

	[SerializeField] [Geocode] string editorGpsLocation;
	public string playerGps
	{ get { return PersistentDataManager.instance.userLocation; } 
	set { PersistentDataManager.instance.userLocation = value; } }

	[SerializeField] GameObject _markerPrefab;
	List<IMapSpawnable> spawnedItems = new List<IMapSpawnable>();

	[SerializeField] MapScholarshipDisplay mapDisplay;
	[SerializeField] GameObject mapObj;

	MapPage _mapPage;
	MapPage mapPage { get { if (_mapPage == null); _mapPage = mapObj.GetComponent<MapPage>(); return _mapPage; } }

	private void Start()
	{
		InvokeRepeating("Tick", 0f, 1f);
	}

	public void SetupDemo()
	{
		//Debug.Log("Setting up demo..." + demoInitialized + ", " + testHunt.prebuiltLevels.Length);
		if (demoInitialized)
			return;

		demoInitialized = true;

		foreach (var level in testHunt.prebuiltLevels)
		{
			currentUser.AddDemoLevel(level);
			AddMarker(true);
		}
	}

	public void SetPositions()
	{
		//Debug.Log("Setting positions..." + spawnedItems.Count + ", " + availableScholarships.Length);
		for (int i = 0; i < spawnedItems.Count; i++)
			spawnedItems[i].SetPosition(availableScholarships[i], mapPage, _map);
	}

	private void Tick()
	{
		//locationData.Tick();
		foreach (var scholarship in availableScholarships)
			scholarship.Tick();

		mapPage.Tick();
	}

	public void AddMarker(bool isDebugLevel)
	{
		//Debug.Log("Add marker method reached. Debug level? " + isDebugLevel);
		// Set default gpsLocation
		string gpsLocation;
		if (isDebugLevel) gpsLocation = currentScholarship.gpsLocation;
		else gpsLocation = editorGpsLocation;

		# if UNITY_EDITOR
		//Debug.Log("Running in Unity Editor...");
		SetScholarshipParameters(currentScholarship, gpsLocation);
		playerGps = editorGpsLocation;
		#endif

		#if UNITY_ANDROID && !UNITY_EDITOR
		//Debug.Log("Running on an Android device");
		StartCoroutine(FindPlayerLocation(currentScholarship, gpsLocation, !isDebugLevel));
		#endif
	}

	//IEnumerator FindPlayerLocation(ScholarshipSet scholarship, bool isDebugLevel)
	IEnumerator FindPlayerLocation(ScholarshipSet scholarship, string gpsLocation, bool isAtPlayer)
	{
		//Debug.Log("Searching for player location..." + Input.location.isEnabledByUser);
		// Check if user has location service enabled
		if (!Input.location.isEnabledByUser)
			yield break;
			
		// Start service before querying location
		Input.location.Start();

		// Wait until the service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			Debug.Log("Searching...");
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service took too long to initialize
		if (maxWait < 1)
		{
			Debug.Log("Location service timed out");
			yield break;
		}

		if (Input.location.status == LocationServiceStatus.Failed)
		{
			Debug.Log("Unable to determine device location");
			yield break;
		}
		else
		{
			gpsLocation = Input.location.lastData.latitude + ", " + Input.location.lastData.longitude;

			if (gpsLocation == "0, 0")
				Debug.Log("Invalid GPS location, using default");

			Input.location.Stop();
		}

		//Debug.Log("Location found! " + gpsLocation);

		if (isAtPlayer)
		{
			playerGps = gpsLocation;
			//Debug.Log("Recording player location at " + playerGps);
		}

		SetScholarshipParameters(scholarship, gpsLocation);

		yield break;
	}

	private void SetScholarshipParameters(ScholarshipSet scholarship, string gpsLocation)
	{
		var duration = MentorSchip.Conversions.StringToInt(scholarship.durationDescription);

		scholarship.gpsLocation = gpsLocation;
		scholarship.fullDuration = duration;
		scholarship.timeRemaining = duration;

		availableScholarships.Add(scholarship);
		var newMarkerObj = Instantiate(_markerPrefab);
		spawnedItems.Add(newMarkerObj.GetComponent<IMapSpawnable>());
		newMarkerObj.transform.parent = mapObj.transform;

		mapDisplay.ShowUI(scholarship);
	}
}