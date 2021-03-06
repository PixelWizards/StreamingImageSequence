﻿using NUnit.Framework;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.StreamingImageSequence;
using UnityEngine.TestTools;

namespace UnityEngine.StreamingImageSequence.Tests {
    public class ImageLoadTest {


        [UnityTest]
        public IEnumerator QueueFullImageLoadTask() {
            StreamingImageSequencePlugin.UnloadAllImages();
            const string PKG_PATH = "Packages/com.unity.streaming-image-sequence/Tests/Data/png/A_00000.png";
            string fullPath = Path.GetFullPath(PKG_PATH);
            Assert.IsTrue(File.Exists(fullPath));

            const int TEX_TYPE = StreamingImageSequenceConstants.IMAGE_TYPE_FULL;

            StreamingImageSequencePlugin.GetImageDataInto(fullPath, TEX_TYPE, Time.frameCount, out ImageData readResult );
            Assert.AreEqual(StreamingImageSequenceConstants.READ_STATUS_UNAVAILABLE, readResult.ReadStatus, 
                "Texture is already or currently being loaded"
            );

            ImageLoadBGTask.Queue(fullPath,Time.frameCount);
            yield return new WaitForSeconds(LOAD_TIMEOUT);
            
            StreamingImageSequencePlugin.GetImageDataInto(fullPath, TEX_TYPE, Time.frameCount, out readResult );
            Assert.AreEqual(StreamingImageSequenceConstants.READ_STATUS_SUCCESS, readResult.ReadStatus,
                "Loading texture is not successful."
            );
            
            AssertUnloaded(fullPath, TEX_TYPE);
            ResetAndAssert(fullPath, TEX_TYPE);
        }

//----------------------------------------------------------------------------------------------------------------------
        [UnityTest]
        public IEnumerator QueuePreviewImageLoadTask() {
            
            StreamingImageSequencePlugin.UnloadAllImages();
            const string PKG_PATH = "Packages/com.unity.streaming-image-sequence/Tests/Data/png/A_00000.png";
            string fullPath = Path.GetFullPath(PKG_PATH);
            Assert.IsTrue(File.Exists(fullPath));

            const int TEX_TYPE = StreamingImageSequenceConstants.IMAGE_TYPE_PREVIEW;

            StreamingImageSequencePlugin.GetImageDataInto(fullPath, TEX_TYPE, Time.frameCount, out ImageData readResult );
            Assert.AreEqual(StreamingImageSequenceConstants.READ_STATUS_UNAVAILABLE, readResult.ReadStatus, 
                "Texture is already or currently being loaded"
            );

            const int WIDTH = 256;
            const int HEIGHT= 128;
            PreviewImageLoadBGTask.Queue(fullPath, WIDTH, HEIGHT, Time.frameCount);
            yield return new WaitForSeconds(LOAD_TIMEOUT);

            StreamingImageSequencePlugin.GetImageDataInto(fullPath,TEX_TYPE, Time.frameCount, out readResult );
            Assert.AreEqual(StreamingImageSequenceConstants.READ_STATUS_SUCCESS, readResult.ReadStatus, 
                "Loading texture is not successful."
            );
            Assert.AreEqual(WIDTH, readResult.Width );
            Assert.AreEqual(HEIGHT, readResult.Height);

            AssertUnloaded(fullPath, TEX_TYPE);
            ResetAndAssert(fullPath, TEX_TYPE);
        }

//----------------------------------------------------------------------------------------------------------------------

        //Check that this image is not loaded yet, except for a particular texture type
        void AssertUnloaded(string fullPath, int exceptionTexType) {
            //Check that we are not affecting the other image types
            for (int texType = 0; texType < StreamingImageSequenceConstants.MAX_IMAGE_TYPES;++texType) {
                if (texType == exceptionTexType)
                    continue;
                StreamingImageSequencePlugin.GetImageDataInto(fullPath, texType, Time.frameCount, 
                    out ImageData otherReadResult);
                Assert.AreEqual(StreamingImageSequenceConstants.READ_STATUS_UNAVAILABLE, otherReadResult.ReadStatus, 
                    "AssertUnloaded()"
                );
            }
        }

//----------------------------------------------------------------------------------------------------------------------

        void ResetAndAssert(string fullPath, int texType) {
            StreamingImageSequencePlugin.UnloadImage(fullPath);
            StreamingImageSequencePlugin.GetImageDataInto(fullPath, texType, Time.frameCount, out ImageData readResult);
            Assert.AreEqual(StreamingImageSequenceConstants.READ_STATUS_UNAVAILABLE, readResult.ReadStatus, "ResetAndAssert");
        }

//----------------------------------------------------------------------------------------------------------------------

        private const float LOAD_TIMEOUT = 3.0f; //in seconds
    }

} //end namespace
