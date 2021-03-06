//
// This file is part of Monobjc, a .NET/Objective-C bridge
// Copyright (C) 2007-2013 - Laurent Etiemble
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//

/**
 * @file    descriptor-System.SByte.mm
 * @brief   Contains the descriptor code to handle the System.SByte type.
 * @author  Laurent Etiemble <laurent.etiemble@monobjc.net>
 * @date    2009-2013
 */
#include "logging.h"
#include "marshal.h"

/**
 * @brief   Allocates a storage zone large enough to hold a marshalled System.SByte.
 * @param   descriptor  The descriptor.
 * @param   native      TRUE if the storage zone has the native type width (direct access)
 *                      or FALSE if the zone is at least the size of pointer storage (as FFI requires it).
 * @return  A pointer to the allocated storage.
 *
 * @remark  The storage zone is at least the size of pointer storage, due to libffi requirement.
 */
static void *__alloc_native_storage_for_System_SByte(MonobjcTypeDescriptor *descriptor, boolean_t native) {
    //LOG_DEBUG(MONOBJC_DOMAIN_MARSHALLING, "__alloc_native_storage_for_System_SByte()");

    if (native) {
        // Allocate a storage zone
        int8_t *ptr = g_new(int8_t, 1);
        return ptr;
    } else {
        // Allocate a storage zone
        int32_t *ptr = g_new(int32_t, 1);
        return ptr;
    }
}

/**
 * @brief   Marshal a native value into a System.SByte object.
 * @param   descriptor  The descriptor.
 * @param   ptr         The pointer to the storage zone that contains the native object.
 * @param   native      TRUE if the storage zone has the native type width (direct access)
 *                      or FALSE if the zone is at least the size of pointer storage (as FFI requires it).
 * @return  A fully initialized System.SByte object.
 *
 * @remark  This function only use the storage zone. It is up to the caller to free it.
 *          Due to libffi requirement, the storage zone used is at least the size of pointer storage.
 *          This mean that smaller types are promoted before being boxed.
 */
static MonoObject *__marshal_from_native_for_System_SByte(MonobjcTypeDescriptor *descriptor, void *ptr, boolean_t native) {
    //LOG_DEBUG(MONOBJC_DOMAIN_MARSHALLING, "__marshal_from_native_for_System_SByte(%p)", ptr);

    if (native) {
        // Promote the value and box it into a managed object
        int8_t value = *((int8_t *) ptr);
        MonoObject *obj = mono_value_box(mono_domain_get(), mono_get_sbyte_class(), &value);
        return obj;
    } else {
        // Promote the value and box it into a managed object
        int32_t value = *((int32_t *) ptr);
        int8_t promoted = (int8_t) value;
        MonoObject *obj = mono_value_box(mono_domain_get(), mono_get_sbyte_class(), &promoted);
        return obj;
    }
}

/**
 * @brief   Marshal a System.SByte object to its native value.
 * @param   descriptor  The descriptor.
 * @param   obj         The managed object.
 * @param   ptr         The pointer to the storage zone that will contain the native object.
 * @param   native      TRUE if the storage zone has the native type width (direct access)
 *                      or FALSE if the zone is at least the size of pointer storage (as FFI requires it).
 *
 * @remark  This function does not allocate the storage zone. It is up to the caller to allocate it and free it.
 */
static void __marshal_to_native_for_System_SByte(MonobjcTypeDescriptor *descriptor, MonoObject *obj, void *ptr, boolean_t native) {
    //LOG_DEBUG(MONOBJC_DOMAIN_MARSHALLING, "__marshal_to_native_for_System_SByte()");

    // Copy the value directly from the unboxed result
    int8_t value = *((int8_t *) mono_object_unbox(obj));
    *(int8_t *)ptr = value;
}

/**
 * @brief   Free the previously allocated storage zone.
 * @param   ptr         The pointer to the storage zone.
 */
static void __free_native_storage_for_System_SByte(void *ptr) {
    //LOG_DEBUG(MONOBJC_DOMAIN_MARSHALLING, "__free_native_storage_for_System_SByte(%p)", ptr);

    // Free the storage zone
    g_free(ptr);
}

MonobjcTypeDescriptor *monobjc_create_descriptor_for_System_SByte() {
    LOG_DEBUG(MONOBJC_DOMAIN_MARSHALLING, "monobjc_create_descriptor_for_System_SByte");
    
    // Allocate the descriptor structure
    MonobjcTypeDescriptor *descriptor = g_new0(MonobjcTypeDescriptor, 1);
    
    // Set the type
    descriptor->type = mono_class_get_type(mono_get_sbyte_class());
    descriptor->boxed = 1;

    // Set the various marshaling functions
    descriptor->alloc_native_storage = __alloc_native_storage_for_System_SByte;
    descriptor->marshal_from_native = __marshal_from_native_for_System_SByte;
    descriptor->marshal_to_native = __marshal_to_native_for_System_SByte;
    descriptor->free_native_storage = __free_native_storage_for_System_SByte;

    // Set the Objective-C encoding
    descriptor->encoding = strdup("c");
    descriptor->size = sizeof(int8_t);
    descriptor->alignment = MIN(log2(descriptor->size), log2(sizeof(void *)));

    // Set the type to use with libffi
    descriptor->foreign_type = &ffi_type_sint8;

    return descriptor;
}
