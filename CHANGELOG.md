# Changelog

## [0.1.5-preview] - 2020-06-01
* chore: depend on com.unity.ext.nunit 
* chore: make it explicit that StreamingImageSequence depends on ugui 

## [0.1.4-preview] - 2020-05-14

* fix: inaccuracies in placing preview icon positions 
* fix: bug when stretching PlayableAsset
* fix: crash bug when entering play mode on Windows 
* fix: remove invalid alerts in StreamingImageSequenceTrack and FaderTrack 
* fix: Override ToString() in StreamingImageSequenceTrack
* chore: update yamato npm registry (#62)

## [0.1.3-preview] - 2020-04-15
* fix: crash caused by performing graphics operation when g_ThreadedGfxDevice is not ready after deserialization	

## [0.1.2-preview] - 2020-04-14
* fix: errors caused by StreamingImageSequenceTrack::GetActivePlayableAsset() when TimelineWindow is not in focus
* fix: keep processing StreamingImageSequencePlayableAsset even if there is no bound GameObject in the track, as the output texture is still required

## [0.1.1-preview] - 2020-04-10
* api: open StreamingImageSequenceTrack to public 
* api: open StreamingImageSequencePlayableAsset::GetTexture() to public 
* docs: Update Japanese docs.

## [0.1.0-preview] - 2020-04-06
* feat: markers to indicate the use/skipping of image in StreamingImageSequencePlayableAsset 

## [0.0.4-preview] - 2020-04-03
* fix: runtime build errors 
* fix: avoid tests from modifying the project assets

## [0.0.3-preview] - 2020-03-27
* fix: update DLLs to avoid the requirement of installing VCRUNTIME140_1.DLL 
* docs: Add Japanese docs

## [0.0.2-preview.3] - 2020-03-16
* docs: Updating img tag to MD

## [0.0.2-preview.2] - 2020-03-05

* feat: Fader imporvements. Reverse FadeOut and FadeIn, and a color to highlight FaderPlayableAsset
* fix: reverse the parameter to copy images to StreamingAssets
* fix: Hide UseImageMarker and use Timeline 1.4.0's ClipCaps.AutoScale 
* fix: StreamingImageSequencePlayableAsset stability issues. 
* fix: Support folder D&D for StreamingImageSequencePlayableAsset from folders which are not under "StreamingAssets"
* fix: Test assembly definitions.

## [0.0.2-preview.1] - 2020-03-02

- Renaming to *Streaming Image Sequence \<com.unity.streaming-image-sequence\>*.
- feat: folder drag and drop support.
- feat: preview icons


## [0.0.1-preview] - 2019-10-10

The first release of *Movie Proxy \<com.unity.movie-proxy\>*.

